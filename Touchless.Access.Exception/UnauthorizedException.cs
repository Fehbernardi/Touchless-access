// =============================================================================
// UnauthorizedException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

namespace Touchless.Access.Exception
{
    /// <summary>
    /// Exceção indicando que a operação não foi autorizada.
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        public UnauthorizedException( string userFriendlyMessage ) : base( userFriendlyMessage )
        {
        }
        #endregion
    }
}