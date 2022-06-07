// =============================================================================
// UnauthorizedError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 20/04/2022
// =============================================================================

using System.Net;

// ReSharper disable UnusedMember.Global

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Classe responsável pela representação dos erros quando a operação não é autorizada.
    /// </summary>
    public class UnauthorizedError : ApiError
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public UnauthorizedError() : base( (int) HttpStatusCode.Unauthorized , HttpStatusCode.Unauthorized.ToString() )
        {
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="message">Mensagem do erro.</param>
        public UnauthorizedError( string message ) : base( (int) HttpStatusCode.Unauthorized , HttpStatusCode.Unauthorized.ToString() , message )
        {
        }
        #endregion
    }
}