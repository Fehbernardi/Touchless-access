// =============================================================================
// RoleService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
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
    public class RoleService : ServiceBase<RoleService>, IRoleService
    {
        #region Variáveis
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IRoleRepository _roleRepository;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="redisCacheClient">Objeto responsável pelo gerenciamento do cache Redis.</param>
        /// <param name="roleRepository">Objeto responsável pelas operaçôes relacionadas as funções dos usuários.</param>
        public RoleService( ILoggerFactory loggerFactory ,
            IRedisCacheClient redisCacheClient , 
            IRoleRepository roleRepository , 
            IConfiguration configuration ) : base( loggerFactory, configuration )
        {
            _redisCacheClient = redisCacheClient ?? throw new ArgumentNullException( nameof(redisCacheClient) );
            _roleRepository = roleRepository ?? throw new ArgumentNullException( nameof(roleRepository) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova função.
        /// </summary>
        /// <param name="role">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<RoleViewModel> AddAsync( RoleViewModel role )
        {
            var roles = await _roleRepository.SearchAsync(
                    new RoleSearch
                    {
                        Name = role.Name
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( roles.Any() ) throw new DuplicateResourceException( "NOME" , "Já existe uma função cadastrada com esse nome." );

            role.CreatedAt = DateTimeOffset.UtcNow;
            return await _roleRepository.AddAsync( role ).ConfigureAwait( false );
        }

        /// <summary>
        /// Retornar os usuários associados a função.
        /// </summary>
        /// <param name="roleId">Identificador único da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<List<UserViewModel>> GetUsersAsync( long roleId )
        {
            var roles = await _roleRepository.SearchAsync(
                    new RoleSearch
                    {
                        Id = roleId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !roles.Any() ) throw new NotFoundException( "Função não localizada." );

            return await _roleRepository.GetUsersAsync( roleId ).ConfigureAwait( false );
        }

        /// <summary>
        /// Retornar o(s) função(es) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das funções.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de funções.</returns>
        public async Task<PagedList<RoleViewModel>> SearchAsync( RoleSearch search , ResourceParameters parameters )
        {
            return await _roleRepository.SearchAsync( search , parameters ).ConfigureAwait( false );
        }

        /// <summary>
        /// Atualizar uma determinda função.
        /// </summary>
        /// <param name="role">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( RoleViewModel role )
        {
            var roles = await _roleRepository.SearchAsync(
                    new RoleSearch
                    {
                        Id = role.Id
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !roles.Any() ) throw new NotFoundException( "Função não localizada." );

            return await _roleRepository.UpdateAsync( role ).ConfigureAwait( false );
        }
        #endregion
    }
}