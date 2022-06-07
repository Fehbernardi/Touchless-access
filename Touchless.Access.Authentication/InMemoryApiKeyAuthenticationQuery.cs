// =============================================================================
// InMemoryApiKeyAuthenticationQuery.cs
// 
// Autor  : Felipe Bernardi
// Data   : 12/05/2022
// =============================================================================

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Touchless.Access.Authentication.Interfaces;
using Touchless.Access.Authentication.Models;

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// Objeto responsável pelo gerenciamento das chave de API em memória.
    /// </summary>
    public class InMemoryApiKeyAuthenticationQuery : IApiKeyAuthenticationQuery
    {
        #region Variáveis
        private readonly IDictionary<string , ApiKey> _apiKeys;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public InMemoryApiKeyAuthenticationQuery()
        {
            var existingApiKeys = new List<ApiKey>
            {
                new("9696AA9D-B8C2-4435-8A70-444D21601D3D") // Zylix
            };

            _apiKeys = existingApiKeys.ToDictionary( x => x.Key , x => x );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Realizar a pesquisa pela existência da chave.
        /// </summary>
        /// <param name="apiKey">Chave a ser pesquisada.</param>
        /// <returns>Chave encontrada.</returns>
        public Task<ApiKey> Execute( string apiKey )
        {
            _apiKeys.TryGetValue( apiKey , out var key );
            return Task.FromResult( key );
        }
        #endregion
    }
}