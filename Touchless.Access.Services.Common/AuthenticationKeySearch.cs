// =============================================================================
// AuthenticationKeySearch.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

namespace Touchless.Access.Services.Common
{
    /// <summary>
    /// Objeto utilizado para especificar os argumentos de busca das chaves de autenticação.
    /// </summary>
    public class AuthenticationKeySearch
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se a chave de autenticação esta ativa ou não.
        /// </summary>
        public bool? Active{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador da chave de autenticação.
        /// </summary>
        public long? Id{ get; set; }
        #endregion
    }
}