// =============================================================================
// IAuthenticationService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using System.Threading.Tasks;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Interfaces
{
    public interface IAuthenticationService
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Realizar o mudança da senha do usuário logado.
        /// </summary>
        /// <param name="changePasswordRequestViewModel">Objeto contendo as informações da novo senha</param>
        /// <returns>Resultado da operação.</returns>
        Task ChangePasswordAsync( ChangePasswordRequestViewModel changePasswordRequestViewModel );

        /// <summary>
        /// Realizar o login de um determinado usuário.
        /// </summary>
        /// <param name="loginRequestViewModel">Objeto contendo as informações do login.</param>
        /// <returns>Resultado da operação.</returns>
        Task<LoginResultViewModel> LoginAsync( LoginRequestViewModel loginRequestViewModel );

        /// <summary>
        /// Realizar o logout do usuário.
        /// </summary>
        /// <param name="userName">Usuário logado.</param>
        /// <returns>Resultado da operação.</returns>
        Task LogoutAsync( string userName );

        /// <summary>
        /// Renovar o token de segurança.
        /// </summary>
        /// <param name="userName">Usuário logado.</param>
        /// <param name="accessToken">Token de acesso.</param>
        /// <param name="claim">Perfil do usuário.</param>
        /// <param name="refreshTokenRequestViewModel">Objeto contendo as informações para a renovação do token.</param>
        /// <returns>Resultado da operação.</returns>
        Task<LoginResultViewModel> RefreshTokenAsync( string userName , string accessToken , string claim , RefreshTokenRequestViewModel refreshTokenRequestViewModel );
        #endregion
    }
}