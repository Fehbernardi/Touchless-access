// =============================================================================
// UserRole.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando as funções dos usuários.
    /// </summary>
    public class UserRoleViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações da função.
        /// </summary>
        public RoleViewModel Role{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador da função.
        /// </summary>
        [Required]
        public long RoleId{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do usuário.
        /// </summary>
        public UserViewModel User{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador do usuário.
        /// </summary>
        [Required]
        public long UserId{ get; set; }
        #endregion
    }
}