// =============================================================================
// RefreshToken.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================
using Newtonsoft.Json;
using System;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Touchless.Access.Authentication.Models
{
    /// <summary>
    /// Objeto contendo as informação do token de renovação.
    /// </summary>
    public class RefreshToken
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar tempo de expiração do token de renovação.
        /// </summary>
        [JsonProperty( "expireAt" )]
        public DateTime ExpireAt{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar conteúdo do token de renovação.
        /// </summary>
        [JsonProperty( "tokenString" )]
        public string TokenString{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar usuário a qual pertence o token de renovação.
        /// </summary>
        [JsonProperty( "userName" )]
        public string UserName{ get; set; }
        #endregion
    }
}