// =============================================================================
// ApiKeyAuthenticationExtensions.cs
// 
// Autor  : Felipe Bernardi
// Data   : 12/08/2022
// =============================================================================
using Microsoft.AspNetCore.Authentication;

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Objeto responsavel por disponibilizar métodos para adicionar o suporte a autenticação através de chave de API.
    /// </summary>
    public static class ApiKeyAuthenticationExtensions
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar o suporte a autenticação através de chave de API.
        /// </summary>
        /// <param name="authenticationBuilder">Objeto responsável pela configuração da autenticação.</param>
        /// <returns>Objeto responsável pela configuração da autenticação.</returns>
        public static AuthenticationBuilder AddApiKeySupport( this AuthenticationBuilder authenticationBuilder )
        {
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions , ApiKeyAuthenticationHandler>( ApiKeyAuthenticationOptions.DefaultScheme , _ => {} );
        }
        #endregion
    }
}