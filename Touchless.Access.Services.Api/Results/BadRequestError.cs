// =============================================================================
// BadRequestError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================
using System.Net;

// ReSharper disable UnusedMember.Global

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Classe responsável pela representação dos erros quando uma requisição é realizada de forma incorreta.
    /// </summary>
    public class BadRequestError : ApiError
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public BadRequestError()
            : base( (int) HttpStatusCode.BadRequest , HttpStatusCode.BadRequest.ToString() )
        {
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="message">Mensagem do erro.</param>
        public BadRequestError( string message )
            : base( (int) HttpStatusCode.BadRequest , HttpStatusCode.BadRequest.ToString() , message )
        {
        }
        #endregion
    }
}