// =============================================================================
// LoginRequestViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 21/04/2022
// =============================================================================

using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando as informações utilizadas no login.
    /// </summary>
    public class LoginRequestViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar senha do usuário.
        /// </summary>
        [Required]
        [DataType( DataType.Password )]
        public string Password{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar usuário.
        /// </summary>
        [Required]
        public string UserName{ get; set; }
        #endregion
    }
}