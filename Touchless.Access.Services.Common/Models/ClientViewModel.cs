// =============================================================================
// CustomerViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 77/04/2022
// =============================================================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando os clientes.
    /// </summary>
    public sealed class ClientViewModel : BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se o cliente esta ativo ou não.
        /// </summary>
        [Required]
        public bool Active{ get; set; }

        /// <summary>
        /// Attribuir/Recuperar os comentários.
        /// </summary>
        public string Comments{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de endereços.
        /// </summary>
        public ICollection<ClientAddressViewModel> ClientAddresses{ get; set; }
        
        /// <summary>
        /// Atribuir/Recuperar a coleção de telefones.
        /// </summary>
        public ICollection<ClientTelephoneViewModel> ClientsTelephones{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar CPF ou CNPJ.
        /// </summary>
        [Required]
        public string FederalIdentification{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        [Required]
        public string Name{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ClientViewModel()
        {
            ClientAddresses = new HashSet<ClientAddressViewModel>();
            ClientsTelephones = new HashSet<ClientTelephoneViewModel>();
        }
        #endregion
    }
}