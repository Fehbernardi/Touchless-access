// =============================================================================
// RefreshTokenRequestViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando as informações da renovação do token de segurança.
    /// </summary>
    public class RefreshTokenRequestViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar token de renovação.
        /// </summary>
        public string RefreshToken{ get; set; }
        #endregion
    }
}