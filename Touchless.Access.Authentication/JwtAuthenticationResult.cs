// =============================================================================
// JwtAuthenticationResult.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

using Newtonsoft.Json;
using Touchless.Access.Authentication.Models;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Objeto contendo o resultado da autenticação JWT.
    /// </summary>
    public class JwtAuthenticationResult
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar token de acesso.
        /// </summary>
        [JsonProperty( "accessToken" )]
        public string AccessToken{ get; set; }

        /// <summary>
        /// Atribuir/Recupera token de renovação.
        /// </summary>
        [JsonProperty( "refreshToken" )]
        public RefreshToken RefreshToken{ get; set; }
        #endregion
    }
}