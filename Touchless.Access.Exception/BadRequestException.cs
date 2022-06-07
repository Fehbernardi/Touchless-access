// =============================================================================
// BadRequestException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

namespace Touchless.Access.Exception
{
    /// <summary>
    /// Exceção indicando que a requisição foi realizada com dados inválidos.
    /// </summary>
    public class BadRequestException : BaseException
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        public BadRequestException( string userFriendlyMessage ) : base( userFriendlyMessage )
        {
        }
        #endregion
    }
}