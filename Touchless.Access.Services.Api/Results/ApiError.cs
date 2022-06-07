// =============================================================================
// ApiError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Classe responsável pela representação dos erros ocorridos nas operações da API.
    /// </summary>
    public class ApiError
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar a mensagem do erro.
        /// </summary>
        [Required]
        [JsonProperty( DefaultValueHandling = DefaultValueHandling.Ignore )]
        public string Message{ get; private set; }

        /// <summary>
        /// Atribuir/Recuperar o código do status HTTP.
        /// </summary>
        [Required]
        public int StatusCode{ get; }

        /// <summary>
        /// Atribuir/Recuperar a descrição do status HTTP.
        /// </summary>
        public string StatusDescription{ get; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="statusCode">Código do status HTTP.</param>
        /// <param name="statusDescription">Descrição do status HTTP.</param>
        public ApiError( int statusCode , string statusDescription )
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="statusCode">Código do status HTTP.</param>
        /// <param name="statusDescription">Descrição do status HTTP.</param>
        /// <param name="message">Mensagem do erro.</param>
        public ApiError( int statusCode , string statusDescription , string message ) : this( statusCode , statusDescription )
        {
            Message = message;
        }
        #endregion
    }
}