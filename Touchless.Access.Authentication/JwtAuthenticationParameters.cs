// =============================================================================
// JwtAuthenticationParameters.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Parâmetros utilizado no processo de autenticação.
    /// </summary>
    public static class JwtAuthenticationParameters
    {
        #region Constantes
        /// <summary>
        /// Nome da audiencia.
        /// </summary>
        public const string Audience = "ZylixAudience";

        /// <summary>
        /// Nome da Entidade.
        /// </summary>
        public const string Issuer = "ZylixIssuer";
        #endregion

        #region Variáveis Estáticas
        /// <summary>
        /// Chaves secretas.
        /// </summary>
        public static readonly byte[] SecretKeys =
        {
            0x11 , 0x1e , 0xc8 , 0xf2 , 0xfd , 0x90 , 0x6e , 0x58 , 0x22 , 0xb0 , 0xce , 0x7a , 0x8e , 0xd8 , 0x9f , 0xff ,
            0xf3 , 0x43 , 0xc4 , 0x0b , 0x8a , 0x57 , 0x7e , 0x1f , 0x0a , 0xeb , 0xf4 , 0xe0 , 0x68 , 0x59 , 0xb8 , 0xb4
        };
        #endregion
    }
}