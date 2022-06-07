// =============================================================================
// AuthenticationKeyRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 18/05/2022
// =============================================================================

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Touchless.Access.Data;
using Touchless.Access.Data.Models;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Pagination;

namespace Touchless.Access.Repository
{
    public class AuthenticationKeyRepository : RepositoryBase , IAuthenticationKeyRepository
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        public AuthenticationKeyRepository( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration ) : base( mapper , applicationContext , configuration )
        {
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
            var newItem = Mapper.Map<AuthenticationKey>( authenticationKey );

            await ApplicationContext.AuthenticationKeys.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<AuthenticationKeyViewModel>( newItem );
        }

        /// <summary>
        /// Remover uma chave de autenticação.
        /// </summary>
        /// <param name="authenticationKeyId">Identificador da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteAsync( long authenticationKeyId )
        {
            return await ApplicationContext.AuthenticationKeys.Where( x => x.Id == authenticationKeyId )
                .DeleteAsync()
                .ConfigureAwait( false ) > 0;
        }

        /// <summary>
        /// Retornar a(s) chave(s) de autenticação que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das chaves de autenticação.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de chaves de autenticação.</returns>
        public async Task<PagedList<AuthenticationKeyViewModel>> SearchAsync( AuthenticationKeySearch search , ResourceParameters parameters )
        {
            var authenticationKeys = ApplicationContext.AuthenticationKeys
                .AsNoTracking()
                .AsQueryable();

            #region Aplicar ordenação
            if( string.IsNullOrWhiteSpace( parameters.OrderBy ) ) parameters.OrderBy = "Label,CreatedAt";

            authenticationKeys = authenticationKeys.ApplySort( parameters.OrderBy );
            #endregion

            #region Aplicar filtro
            if( search?.Active != null ) authenticationKeys = authenticationKeys.Where( x => x.Active == search.Active.Value );

            if( search?.Id != null ) authenticationKeys = authenticationKeys.Where( x => x.Id == search.Id.Value );
            #endregion

            return Mapper.Map<PagedList<AuthenticationKeyViewModel>>( await PagedList<AuthenticationKey>.CreateAsync( authenticationKeys , parameters.PageNumber , parameters.PageSize ).ConfigureAwait( false ) );
        }

        /// <summary>
        /// Atualizar uma determinda chave de autenticação.
        /// </summary>
        /// <param name="authenticationKey">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( AuthenticationKeyViewModel authenticationKey )
        {
            return await ApplicationContext.AuthenticationKeys.Where( x => x.Id == authenticationKey.Id )
                .UpdateAsync( x => new AuthenticationKey
                {
                    Active = authenticationKey.Active ,
                    Label = authenticationKey.Label
                } ).ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}