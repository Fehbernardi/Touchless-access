// =============================================================================
// IApiKeyAuthenticationQuery.cs
// 
// Autor  : Felipe Bernardi
// Data   : 12/06/2022
// =============================================================================
using System.Threading.Tasks;
using Touchless.Access.Authentication.Models;

namespace Touchless.Access.Authentication.Interfaces
{
    /// <summary>
    /// Disponibiliza os métodos responsáveis pela pequisa da chave de API.
    /// </summary>
    public interface IApiKeyAuthenticationQuery
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Realizar a pesquisa pela existência da chave.
        /// </summary>
        /// <param name="apiKey">Chave a ser pesquisada.</param>
        /// <returns>Chave encontrada.</returns>
        Task<ApiKey> Execute( string apiKey );
        #endregion
    }
}