// =============================================================================
// ForbiddenException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

namespace Touchless.Access.Exception
{
    /// <summary>
    /// Exceção indicando que a operação foi negada.
    /// </summary>
    public class ForbiddenException : BaseException
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        public ForbiddenException( string userFriendlyMessage ) : base( userFriendlyMessage )
        {
        }
        #endregion
    }
}