// =============================================================================
// ChangePasswordRequestViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 26/04/2022
// =============================================================================

using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando as informações para a mudança da senha.
    /// </summary>
    public class ChangePasswordRequestViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar nova senha do usuário.
        /// </summary>
        [Required]
        [DataType( DataType.Password )]
        public string NewPassword{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar senha antiga do usuário.
        /// </summary>
        [Required]
        [DataType( DataType.Password )]
        public string OldPassword{ get; set; }
        #endregion
    }
}