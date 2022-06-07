// =============================================================================
// ApiKey.cs
// 
// Autor  : Felipe Bernardi
// Data   : 04/05/2022
// =============================================================================
using Newtonsoft.Json;
using System;

namespace Touchless.Access.Authentication.Models
{
    /// <summary>
    /// Objeto contendo as informações sobre a chave de acesso.
    /// </summary>
    public class ApiKey
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar a chave de acesso.
        /// </summary>
        [JsonProperty( "key" )]
        public string Key{ get; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="key">Chave de acesso.</param>
        public ApiKey( string key )
        {
            Key = key ?? throw new ArgumentNullException( nameof(key) );
        }
        #endregion
    }
}