// =============================================================================
// RoRoleViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================
using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando as funções.
    /// </summary>
    public sealed class RoleViewModel : BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        [Required]
        public string Name{ get; set; }
        #endregion
    }
}