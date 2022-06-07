// =============================================================================
// CustomerAddressViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando os endereços dos clientes.
    /// </summary>
    public sealed class ClientAddressViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do endereço.
        /// </summary>
        public AddressViewModel Address{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do cliente.
        /// </summary>
        public ClientViewModel Customer{ get; set; }
        #endregion
    }
}