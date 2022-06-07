// =============================================================================
// BaseException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

using System;

namespace Touchless.Access.Exception
{
    /// <summary>
    /// Exceção base para as demais criadas para a aplicação.
    /// </summary>
    public class BaseException : ApplicationException
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        protected BaseException( string userFriendlyMessage ) : base( userFriendlyMessage )
        {
        }
        #endregion
    }
}