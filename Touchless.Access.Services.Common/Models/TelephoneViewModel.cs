// =============================================================================
// TelephoneViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando os telefones.
    /// </summary>
    public sealed class TelephoneViewModel : BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar código do país.
        /// </summary>
        [Required]
        public string CountryCode{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar número do telefone.
        /// </summary>
        [Required]
        public string Number{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar código da região.
        /// </summary>
        [Required]
        public string RegionCode{ get; set; }
        #endregion
    }
}