// =============================================================================
// ConflictError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================
using System.Net;

// ReSharper disable UnusedMember.Global

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Classe responsável pela representação dos erros quando um recurso já existe.
    /// </summary>
    public class ConflictError : ApiError
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ConflictError()
            : base( (int) HttpStatusCode.Conflict , HttpStatusCode.Conflict.ToString() )
        {
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="message">Mensagem do erro.</param>
        public ConflictError( string message )
            : base( (int) HttpStatusCode.Conflict , HttpStatusCode.Conflict.ToString() , message )
        {
        }
        #endregion
    }
}