// =============================================================================
// AddressViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando os endereços.
    /// </summary>
    public sealed class AddressViewModel : BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar a cidade.
        /// </summary>
        [Required]
        public string City{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar dados complementares.
        /// </summary>
        public string Complement{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o rótulo.
        /// </summary>
        [Required]
        public string Label{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o número.
        /// </summary>
        public int? Number{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o CEP.
        /// </summary>
        [Required]
        public string PostalCode{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o Estado.
        /// </summary>
        [Required]
        public string State{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o nome da rua.
        /// </summary>
        [Required]
        public string Street{ get; set; }
        #endregion
    }
}