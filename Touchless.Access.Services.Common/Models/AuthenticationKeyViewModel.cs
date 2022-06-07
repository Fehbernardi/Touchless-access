// =============================================================================
// AuthenticationKeyViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 02/05/2022
// =============================================================================

using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando a tabela AuthenticationKey.
    /// </summary>
    public sealed class AuthenticationKeyViewModel : BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se a chave esta ativa ou não.
        /// </summary>
        [Required]
        public bool Active{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o código.
        /// </summary>
        public string Code{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o rótulo.
        /// </summary>
        [Required]
        public string Label{ get; set; }
        #endregion
    }
}