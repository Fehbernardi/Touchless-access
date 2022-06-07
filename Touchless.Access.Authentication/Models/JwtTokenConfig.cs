// =============================================================================
// JwtTokenConfig.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================
using Newtonsoft.Json;

// ReSharper disable ClassNeverInstantiated.Global

namespace Touchless.Access.Authentication.Models
{
    /// <summary>
    /// Objeto contendo as informações sobre o token de acesso.
    /// </summary>
    public class JwtTokenConfig
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar tempo em minutos da validade do token de acesso.
        /// </summary>
        [JsonProperty( "accessTokenExpiration" )]
        public int AccessTokenExpiration{ get; set; } = 30;

        /// <summary>
        /// Atribuir/Recuperar tempo em minutos que o token de renovação irá expirar.
        /// </summary>
        [JsonProperty( "refreshTokenExpiration" )]
        public int RefreshTokenExpiration{ get; set; } = 60;
        #endregion
    }
}