// =============================================================================
// clientsearch.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common
{
    /// <summary>
    /// Objeto utilizado para especificar os argumentos de busca dos clientes.
    /// </summary>
    public class ClientSearch
    {
        #region Propriedades Públicas

        /// <summary>
        /// Atribuir/Recuperar indicativo se o cliente esta ativo ou não.
        /// </summary>
        public bool? Active{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar CPF ou CNPJ.
        /// </summary>
        public string FederalIdentification{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador do cliente.
        /// </summary>
        public long? Id{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        public string Name{ get; set; }

        /// <summary>
        /// Indicativo informando que deve ser adicionado as informações complementares.
        /// </summary>
        public bool IncludeAll { get; set; } = false;
        #endregion
    }
}