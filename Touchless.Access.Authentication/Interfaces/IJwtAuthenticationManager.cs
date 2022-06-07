// =============================================================================
// IJwtAuthenticationManager.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

// ReSharper disable UnusedMemberInSuper.Global

namespace Touchless.Access.Authentication.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IJwtAuthenticationManager
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Decodificar o token JWT.
        /// </summary>
        /// <param name="token">Conteúdo do token.</param>
        /// <returns>Resultado da operação.</returns>
        (ClaimsPrincipal , JwtSecurityToken) DecodeJwtToken( string token );

        /// <summary>
        /// Gerar o token de autenticação.
        /// </summary>
        /// <param name="userName">Usuário</param>
        /// <param name="claims">Claims de segurança.</param>
        /// <returns>Resultado da operação.</returns>
        Task<JwtAuthenticationResult> GenerateTokensAsync( string userName , Claim[] claims );

        /// <summary>
        /// Atualizar o token de segurança.
        /// </summary>
        /// <param name="refreshToken">Conteúdo do token de segurança.</param>
        /// <param name="accessToken">Conteúdo do token de acesso.</param>
        /// <returns>Resultado da operação.</returns>
        Task<JwtAuthenticationResult> RefreshAsync( string refreshToken , string accessToken );

        /// <summary>
        /// Remover o token de renovação.
        /// </summary>
        /// <param name="userName">Usuário a qual o token de renovação pertence.</param>
        Task RemoveRefreshTokenByUserNameAsync( string userName );
        #endregion
    }
}