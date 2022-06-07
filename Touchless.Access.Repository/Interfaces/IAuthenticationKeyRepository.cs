// =============================================================================
// IAuthenticationKeyRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 25/04/2022
// =============================================================================

using System.Threading.Tasks;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository.Interfaces
{
    public interface IAuthenticationKeyRepository
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova chave de autenticação.
        /// </summary>
        /// <param name="authenticationKey">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        Task<AuthenticationKeyViewModel> AddAsync( AuthenticationKeyViewModel authenticationKey );

        /// <summary>
        /// Remover uma chave de autenticação.
        /// </summary>
        /// <param name="authenticationKeyId">Identificador da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> DeleteAsync( long authenticationKeyId );

        /// <summary>
        /// Retornar a(s) chave(s) de autenticação que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das chaves de autenticação.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de chaves de autenticação.</returns>
        Task<PagedList<AuthenticationKeyViewModel>> SearchAsync( AuthenticationKeySearch search , ResourceParameters parameters );

        /// <summary>
        /// Atualizar uma determinda chave de autenticação.
        /// </summary>
        /// <param name="authenticationKey">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAsync( AuthenticationKeyViewModel authenticationKey );
        #endregion
    }
}