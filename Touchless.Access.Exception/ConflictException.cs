// =============================================================================
// ConflictException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/06/2022
// =============================================================================


namespace Touchless.Access.Exception
{
    public class ConflictException : BaseException
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        public ConflictException( string userFriendlyMessage ) : base( userFriendlyMessage )
        {
        }
        #endregion
    }
}