// =============================================================================
// NotFoundError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================
using System.Net;

// ReSharper disable UnusedMember.Global

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Classe responsável pela representação dos erros quando um recurso não é encontrado.
    /// </summary>
    public class NotFoundError : ApiError
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public NotFoundError()
            : base( (int) HttpStatusCode.NotFound , HttpStatusCode.NotFound.ToString() )
        {
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="message">Mensagem do erro.</param>
        public NotFoundError( string message )
            : base( (int) HttpStatusCode.NotFound , HttpStatusCode.NotFound.ToString() , message )
        {
        }
        #endregion
    }
}