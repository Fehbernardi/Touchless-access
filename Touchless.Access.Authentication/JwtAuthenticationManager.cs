// =============================================================================
// JwtAuthenticationManager.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Touchless.Access.Authentication.Interfaces;
using Touchless.Access.Authentication.Models;
namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Objeto responsável pelo gerenciamento da autenticação JWT.
    /// </summary>
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        #region Variáveis
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly IRedisCacheClient _redisCacheClient;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="redisCacheClient">Objeto responsável pelo gerenciamento do cache Redis.</param>
        /// <param name="jwtTokenConfig">Objeto responsável pela configuração do token de acesso.</param>
        public JwtAuthenticationManager( IRedisCacheClient redisCacheClient , JwtTokenConfig jwtTokenConfig )
        {
            _redisCacheClient = redisCacheClient ?? throw new ArgumentNullException( nameof(redisCacheClient) );
            _jwtTokenConfig = jwtTokenConfig ?? throw new ArgumentNullException( nameof(jwtTokenConfig) );
        }
        #endregion

        #region Métodos/Operadores Privados
        /// <summary>
        /// Criar um token de renovação.
        /// </summary>
        /// <returns>Token de renovação.</returns>
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes( randomNumber );
            return Convert.ToBase64String( randomNumber );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Decodificar o token JWT.
        /// </summary>
        /// <param name="token">Conteúdo do token.</param>
        /// <returns>Resultado da operação.</returns>
        public (ClaimsPrincipal , JwtSecurityToken) DecodeJwtToken( string token )
        {
            if( string.IsNullOrWhiteSpace( token ) ) throw new SecurityTokenException( "Token inválido" );

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken( token , new TokenValidationParameters
                {
                    ValidateIssuer = true ,
                    ValidIssuer = JwtAuthenticationParameters.Issuer ,
                    ValidateIssuerSigningKey = true ,
                    IssuerSigningKey = new SymmetricSecurityKey( JwtAuthenticationParameters.SecretKeys ) ,
                    ValidAudience = JwtAuthenticationParameters.Audience ,
                    ValidateAudience = true ,
                    ValidateLifetime = true ,
                    ClockSkew = TimeSpan.FromSeconds( 30 )
                } , out var validatedToken );
            return (principal , validatedToken as JwtSecurityToken);
        }

        /// <summary>
        /// Gerar o token de autenticação.
        /// </summary>
        /// <param name="userName">Usuário</param>
        /// <param name="claims">Claims de segurança.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<JwtAuthenticationResult> GenerateTokensAsync( string userName , Claim[] claims )
        {
            var now = DateTime.Now;

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace( claims?.FirstOrDefault( x => x.Type == JwtRegisteredClaimNames.Aud )?.Value );
            var jwtToken = new JwtSecurityToken(
                JwtAuthenticationParameters.Issuer ,
                shouldAddAudienceClaim ? JwtAuthenticationParameters.Audience : string.Empty ,
                claims ,
                expires:now.AddMinutes( _jwtTokenConfig.AccessTokenExpiration ) ,
                signingCredentials:new SigningCredentials( new SymmetricSecurityKey( JwtAuthenticationParameters.SecretKeys ) , SecurityAlgorithms.HmacSha256Signature ) );
            var accessToken = new JwtSecurityTokenHandler().WriteToken( jwtToken );

            var refreshToken = new RefreshToken
            {
                UserName = userName ,
                TokenString = GenerateRefreshTokenString() ,
                ExpireAt = now.AddMinutes( _jwtTokenConfig.RefreshTokenExpiration )
            };

            var result = new JwtAuthenticationResult
            {
                AccessToken = accessToken ,
                RefreshToken = refreshToken
            };

            await _redisCacheClient.Db0.AddAsync( $"token:{refreshToken.UserName}" , result , TimeSpan.FromMinutes( _jwtTokenConfig.RefreshTokenExpiration ) ).ConfigureAwait( false );
            return result;
        }

        /// <summary>
        /// Atualizar o token de segurança.
        /// </summary>
        /// <param name="refreshToken">Conteúdo do token de segurança.</param>
        /// <param name="accessToken">Conteúdo do token de acesso.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<JwtAuthenticationResult> RefreshAsync( string refreshToken , string accessToken )
        {
            var now = DateTime.Now;

            var (principal , jwtToken) = DecodeJwtToken( accessToken );
            if( jwtToken == null || !jwtToken.Header.Alg.Equals( SecurityAlgorithms.HmacSha256Signature ) ) throw new SecurityTokenException( "Token inválido" );

            var userName = principal.Identity?.Name;
            if( !await _redisCacheClient.Db0.ExistsAsync( $"token:{userName}" ).ConfigureAwait( false ) ) throw new SecurityTokenException( "Token inválido" );

            var existingToken = await _redisCacheClient.Db0.GetAsync<JwtAuthenticationResult>( $"token:{userName}" ).ConfigureAwait( false );

            if( !existingToken.RefreshToken.TokenString.Equals( refreshToken ) ) throw new SecurityTokenException( "Token inválido" );
            if( existingToken.RefreshToken.UserName != userName || existingToken.RefreshToken.ExpireAt < now ) throw new SecurityTokenException( "Token inválido" );

            return await GenerateTokensAsync( userName , principal.Claims.ToArray() ).ConfigureAwait( false );
        }

        /// <summary>
        /// Remover o token de renovação.
        /// </summary>
        /// <param name="userName">Usuário a qual o token de renovação pertence.</param>
        public async Task RemoveRefreshTokenByUserNameAsync( string userName )
        {
            await _redisCacheClient.Db0.RemoveAsync( $"token:{userName}" ).ConfigureAwait( false );
        }
        #endregion
    }
}