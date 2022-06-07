// =============================================================================
// InternalServerError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================
using System.Net;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Classe responsável pela representação dos erros internos ocorridos no servidor.
    /// </summary>
    public class InternalServerError : ApiError
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public InternalServerError() :
            base( (int) HttpStatusCode.InternalServerError , HttpStatusCode.InternalServerError.ToString() )
        {
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="message">Mensagem do erro.</param>
        public InternalServerError( string message ) :
            base( (int) HttpStatusCode.InternalServerError , HttpStatusCode.InternalServerError.ToString() , message )
        {
        }
        #endregion
    }
}