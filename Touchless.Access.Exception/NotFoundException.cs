// =============================================================================
// NotFoundException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

namespace Touchless.Access.Exception
{
    /// <summary>
    /// Exceção indicando que o registro não foi localizado.
    /// </summary>
    public class NotFoundException : BaseException
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        public NotFoundException( string userFriendlyMessage ) : base( userFriendlyMessage )
        {
        }
        #endregion
    }
}