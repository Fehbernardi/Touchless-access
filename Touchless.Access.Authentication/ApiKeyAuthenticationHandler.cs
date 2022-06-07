// =============================================================================
// ApiKeyAuthenticationHandler.cs
// 
// Autor  : Felipe Bernardi
// Data   : 12/05/2022
// =============================================================================
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Touchless.Access.Authentication.Interfaces;

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Objeto responsável pelo gerenciamento da autenticação por chave de API.
    /// </summary>
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        #region Constantes
        private const string ApiKeyHeaderName = "access-api-key";
        private const string ProblemDetailsContentType = "application/problem+json";
        #endregion

        #region Variáveis
        private readonly IApiKeyAuthenticationQuery _apiKeyAuthenticationQuery;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padr/ao.
        /// </summary>
        /// <param name="options">Objeto responsavel pelas opções do processo de autenticação.</param>
        /// <param name="logger">Objeto responsável pelo Log.</param>
        /// <param name="encoder">Objeto responsável pelo encoding da URL.</param>
        /// <param name="clock">Objeto contendo informações sobre o relógio.</param>
        /// <param name="apiKeyAuthenticationQuery">Objeto responsável pela pesquisa das chaves de API.</param>
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options ,
            ILoggerFactory logger ,
            UrlEncoder encoder ,
            ISystemClock clock ,
            IApiKeyAuthenticationQuery apiKeyAuthenticationQuery
        ) : base( options , logger , encoder , clock )
        {
            _apiKeyAuthenticationQuery = apiKeyAuthenticationQuery ?? throw new ArgumentNullException( nameof(apiKeyAuthenticationQuery) );
        }
        #endregion

        #region Métodos/Operadores Protegidos
        /// <summary>
        /// Realizar o processo de autenticação.
        /// </summary>
        /// <returns>Resultado da operação.</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if( !Request.Headers.TryGetValue( ApiKeyHeaderName , out var apiKeyHeaderValues ) ) return AuthenticateResult.NoResult();

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if( apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace( providedApiKey ) ) return AuthenticateResult.NoResult();

            var existingApiKey = await _apiKeyAuthenticationQuery.Execute( providedApiKey );

            if( existingApiKey == null ) return AuthenticateResult.Fail( "A chave de API fornecida é inválida." );
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name , existingApiKey.Key) ,
                new(ClaimTypes.Role , "admin")
            };

            var identity = new ClaimsIdentity( claims , ApiKeyAuthenticationOptions.AuthenticationType );
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal( identities );
            var ticket = new AuthenticationTicket( principal , ApiKeyAuthenticationOptions.Scheme );

            return AuthenticateResult.Success( ticket );
        }

        /// <summary>
        /// Realizar o processo de desafio da autenticação.
        /// </summary>
        /// <param name="properties">Objeto contendo informaçõesso sobre a sessão de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        protected override async Task HandleChallengeAsync( AuthenticationProperties properties )
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized ,
                Detail = "Não autorizado."
            };

            await Response.WriteAsync( JsonConvert.SerializeObject( problemDetails ) ).ConfigureAwait( false );
        }

        /// <summary>
        /// Realizar o processo quando uma autenticação for negada.
        /// </summary>
        /// <param name="properties">Objeto contendo informaçõesso sobre a sessão de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        protected override async Task HandleForbiddenAsync( AuthenticationProperties properties )
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.Forbidden ,
                Detail = "Operação recusada pelo servidor."
            };

            await Response.WriteAsync( JsonConvert.SerializeObject( problemDetails ) ).ConfigureAwait( false );
        }
        #endregion
    }
}