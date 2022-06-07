// =============================================================================
// UserViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando os usuários.
    /// </summary>
    public sealed class UserViewModel : BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se o usuário esta ativo ou não.
        /// </summary>
        [Required]
        public bool Active{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        [Required]
        public string Name{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a senha do usuário.
        /// </summary>
        public string Password{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar hash calculado da senha.
        /// </summary>
        public string PasswordHash{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar usuário.
        /// </summary>
        [Required]
        public string Username{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de funções do usuário.
        /// </summary>
        public ICollection<UserRoleViewModel> UserRoles{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public UserViewModel()
        {
            UserRoles = new HashSet<UserRoleViewModel>();
        }
        #endregion
    }
}