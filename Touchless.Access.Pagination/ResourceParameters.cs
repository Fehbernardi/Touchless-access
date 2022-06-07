// =============================================================================
// ResourceParameters.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/06/2019
// =============================================================================

namespace Touchless.Access.Pagination
{
    /// <summary>
    /// Objeto contendo informações da paginação.
    /// </summary>
    public class ResourceParameters
    {
        #region Constantes
        private const int MaxPageSize = 500;
        #endregion

        #region Variáveis
        private int _pageSize;
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar a ordenação dos dados.
        /// </summary>
        public string OrderBy{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o número da página.
        /// </summary>
        public int PageNumber{ get; set; } = 1;

        /// <summary>
        /// Atribuir/Recuperar o tamanho da página.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        #endregion
    }
}