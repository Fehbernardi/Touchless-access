// =============================================================================
// ApiKeyAuthenticationOptions.cs
// 
// Autor  : Felipe Bernardi
// Data   : 12/05/2022
// =============================================================================
using Microsoft.AspNetCore.Authentication;

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Objeto contendo as informações para realização da autenticação por chave de API.
    /// </summary>
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        #region Constantes
        /// <summary>
        /// Tipo da autenticação.
        /// </summary>
        public const string AuthenticationType = DefaultScheme;

        /// <summary>
        /// Schema padrão.
        /// </summary>
        public const string DefaultScheme = "ApiKey";
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Atribuir o schema.
        /// </summary>
        public static string Scheme => DefaultScheme;
        #endregion
    }
}