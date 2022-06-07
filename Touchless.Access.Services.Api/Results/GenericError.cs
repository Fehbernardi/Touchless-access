// =============================================================================
// GenericError.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Touchless.Access.Services.Api.Results
{
    /// <summary>
    /// Objeto responsável pela representação do erro de processamento da requisição.
    /// </summary>
    public class GenericError
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar coleção de informações mais específicas sobre o erro.
        /// </summary>
        [JsonProperty( "innerError" )]
        public List<GenericError> InnerError{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a descrição do erro.
        /// </summary>
        [Required]
        [JsonProperty( "message" )]
        public string Message{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a pilha de execução.
        /// </summary>
        [JsonProperty( "stackTrace" )]
        public string StackTrace{ get; set; }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Retornar uma string que representa o objeto corrente.
        /// </summary>
        /// <returns>String que representa o objeto corrente.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject( this );
        }
        #endregion
    }
}