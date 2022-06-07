// =============================================================================
// UserSearch.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common
{
    /// <summary>
    /// Objeto utilizado para especificar os argumentos de busca dos usuários.
    /// </summary>
    public class UserSearch
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se o usuário esta ativo ou não.
        /// </summary>
        public bool? Active{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador do usuário.
        /// </summary>
        public long? Id{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        public string Name{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar usuário.
        /// </summary>
        public string Username{ get; set; }
        #endregion
    }
}