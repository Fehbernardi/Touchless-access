// =============================================================================
// LoginResultViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando as informações do resultado do processo de login.
    /// </summary>
    public class LoginResultViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar o token de acesso.
        /// </summary>
        public string AccessToken{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o refresh token.
        /// </summary>
        public string RefreshToken{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a função a qual o usuário pertence.
        /// </summary>
        public string Role{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar usuário.
        /// </summary>
        public string UserName{ get; set; }
        #endregion
    }
}