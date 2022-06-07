// =============================================================================
// CustomerTelephoneViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto representando os telefones dos clientes.
    /// </summary>
    public sealed class ClientTelephoneViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do cliente.
        /// </summary>
        public ClientViewModel Customer{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do telefone.
        /// </summary>
        public TelephoneViewModel Telephone{ get; set; }
        #endregion
    }
}