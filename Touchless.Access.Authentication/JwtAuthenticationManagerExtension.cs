// =============================================================================
// JwtAuthenticationManagerExtension.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading.Tasks;
using Touchless.Access.Authentication.Interfaces;
using Touchless.Access.Authentication.Models;

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// </summary>
    public static class JwtAuthenticationManagerExtension
    {
        #region Variáveis Estáticas
        private static IRedisCacheClient _redisCacheClient;
        #endregion

        #region Métodos/Operadores Privados
        /// <summary>
        /// Validar o token de segurança da requisição.
        /// </summary>
        /// <param name="userName">Nome do usuário.</param>
        /// <param name="accessToken">Conteúdo do token de acesso.</param>
        /// <returns>Verdadeiro se válido, caso contrário, falso.</returns>
        private static async Task<bool> ValidateAccessTokenAsync( string userName , string accessToken )
        {
            if( !await _redisCacheClient.Db0.ExistsAsync( $"token:{userName}" ).ConfigureAwait( false ) ) return false;

            var existingToken = await _redisCacheClient.Db0.GetAsync<JwtAuthenticationResult>( $"token:{userName}" ).ConfigureAwait( false );
            return existingToken.AccessToken.Equals( accessToken );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar a autenticação JWT.
        /// </summary>
        /// <param name="services">Objeto responsável pelo gerenciamento dos serviços da aplicação.</param>
        /// <param name="jwtTokenConfig">Objeto contendo as configurações do token.</param>
        /// <param name="addApiKeySupport">Indicativo informando que deve ser adicionado o suporte a autenticação por chave de API.</param>
        public static void AddJwtAuthentication( this IServiceCollection services , JwtTokenConfig jwtTokenConfig , bool addApiKeySupport = true )
        {
            services.AddSingleton( jwtTokenConfig );
            services.AddSingleton<IJwtAuthenticationManager , JwtAuthenticationManager>();
            services.AddSingleton<IApiKeyAuthenticationQuery , InMemoryApiKeyAuthenticationQuery>();

            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder( JwtBearerDefaults.AuthenticationScheme );

            var authenticaionBuilder = services.AddAuthentication( options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } );

            if( addApiKeySupport )
            {
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.AddAuthenticationSchemes( "ApiKey" );
                authenticaionBuilder.AddApiKeySupport();
            }

            authenticaionBuilder.AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true ,
                    ValidateAudience = true ,
                    ValidateLifetime = true ,
                    ValidateIssuerSigningKey = true ,
                    ValidIssuer = JwtAuthenticationParameters.Issuer ,
                    ValidAudience = JwtAuthenticationParameters.Audience ,
                    IssuerSigningKey = new SymmetricSecurityKey( JwtAuthenticationParameters.SecretKeys ) ,
                    ClockSkew = TimeSpan.FromSeconds( 30 )
                };

                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add( new JwtAuthenticationSecurityTokenHandler
                {
                    ValidateAccessToken = ValidateAccessTokenAsync
                } );
            } );

            services.AddAuthorization( options =>
            {
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            } );
        }

        /// <summary>
        /// Configurar a autenticação JWT.
        /// </summary>
        /// <param name="application">Ojbeto responsável pela configuração do pipeline de requisição da aplicação.</param>
        public static void ConfigureJwtAuthentication( this IApplicationBuilder application )
        {
            _redisCacheClient = application.ApplicationServices.GetService<IRedisCacheClient>();
        }
        #endregion
    }
}