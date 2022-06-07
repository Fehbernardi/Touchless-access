// =============================================================================
// AuthenticationKeyService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 16/05/2022
// =============================================================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Services.Interfaces;

// ReSharper disable NotAccessedField.Local

namespace Touchless.Access.Services
{
    public class AuthenticationKeyService : ServiceBase<AuthenticationKeyService>, IAuthenticationKeyService
    {
        #region Variáveis
        private readonly IAuthenticationKeyRepository _authenticationKeyRepository;
        private readonly IRedisCacheClient _redisCacheClient;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="redisCacheClient">Objeto responsável pelo gerenciamento do cache Redis.</param>
        /// <param name="authenticationKeyRepository">Objeto responsável pelas operaçôes relacionadas as chaves de autenticação.</param>
        public AuthenticationKeyService( ILoggerFactory loggerFactory ,
            IRedisCacheClient redisCacheClient , 
            IAuthenticationKeyRepository authenticationKeyRepository , 
            IConfiguration configuration ) : base( loggerFactory, configuration )
        {
            _redisCacheClient = redisCacheClient ?? throw new ArgumentNullException( nameof(redisCacheClient) );
            _authenticationKeyRepository = authenticationKeyRepository ?? throw new ArgumentNullException( nameof(authenticationKeyRepository) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova chave de autenticação.
        /// </summary>
        /// <param name="authenticationKey">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<AuthenticationKeyViewModel> AddAsync( AuthenticationKeyViewModel authenticationKey )
        {
            authenticationKey.CreatedAt = DateTimeOffset.UtcNow;
            return await _authenticationKeyRepository.AddAsync( authenticationKey ).ConfigureAwait( false );
        }
        /// <summary>
        /// Remover uma chave de autenticação.
        /// </summary>
        /// <param name="authenticationKeyId">Identificador da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteAsync( long authenticationKeyId )
        {
            return await _authenticationKeyRepository.DeleteAsync( authenticationKeyId ).ConfigureAwait( false );
        }

        /// <summary>
        /// Retornar a(s) chave(s) de autenticação que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das chaves de autenticação.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de chaves de autenticação.</returns>
        public async Task<PagedList<AuthenticationKeyViewModel>> SearchAsync( AuthenticationKeySearch search , ResourceParameters parameters )
        {
            return await _authenticationKeyRepository.SearchAsync( search , parameters ).ConfigureAwait( false );
        }

        /// <summary>
        /// Atualizar uma determinda chave de autenticação.
        /// </summary>
        /// <param name="authenticationKey">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( AuthenticationKeyViewModel authenticationKey )
        {
            var authenticationKeys = await _authenticationKeyRepository.SearchAsync(
                    new AuthenticationKeySearch
                    {
                        Id = authenticationKey.Id
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !authenticationKeys.Any() ) throw new NotFoundException( "Chave de autenticação não localizada." );

            return await _authenticationKeyRepository.UpdateAsync( authenticationKey ).ConfigureAwait( false );
        }
        #endregion
    }
}