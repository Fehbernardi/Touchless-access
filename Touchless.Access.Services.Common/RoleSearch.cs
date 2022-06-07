// =============================================================================
// RoleSearch.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

namespace Touchless.Access.Services.Common
{
    /// <summary>
    /// Objeto utilizado para especificar os argumentos de busca das funções.
    /// </summary>
    public class RoleSearch
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar identificador da função.
        /// </summary>
        public long? Id{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        public string Name{ get; set; }
        #endregion
    }
}