// =============================================================================
// UserRepository.cs
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
using Touchless.Access.Pagination;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository
{
    public partial class UserRepository : RepositoryBase , IUserRepository
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        public UserRepository( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration ) : base( mapper , applicationContext , configuration )
        {
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo usuário.
        /// </summary>
        /// <param name="user">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<UserViewModel> AddAsync( UserViewModel user )
        {
            var newItem = Mapper.Map<User>( user );

            await ApplicationContext.Users.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<UserViewModel>( newItem );
        }

        /// <summary>
        /// Retornar o(s) usuário(s) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos usuários.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de usuários.</returns>
        public async Task<PagedList<UserViewModel>> SearchAsync( UserSearch search , ResourceParameters parameters )
        {
            var users = ApplicationContext.Users
                .Include( x => x.UserRoles )
                .ThenInclude( x => x.Role )
                .AsNoTracking()
                .AsQueryable();

            #region Aplicar ordenação
            if( string.IsNullOrWhiteSpace( parameters.OrderBy ) ) parameters.OrderBy = "Name,CreatedAt";

            users = users.ApplySort( parameters.OrderBy );
            #endregion

            #region Aplicar filtro
            if( search?.Active != null ) users = users.Where( x => x.Active == search.Active.Value );

            if( search?.Id != null ) users = users.Where( x => x.Id == search.Id.Value );

            if( !string.IsNullOrWhiteSpace( search?.Name ) )
            {
                var likeExpression = $"%{search.Name}%";
                users = users.Where( x => EF.Functions.ILike( x.Name , likeExpression ) );
            }

            if( !string.IsNullOrWhiteSpace( search?.Username ) )
            {
                users = users.Where( x => x.Username == search.Username );
            }
            #endregion

            return Mapper.Map<PagedList<UserViewModel>>( await PagedList<User>.CreateAsync( users , parameters.PageNumber , parameters.PageSize ).ConfigureAwait( false ) );
        }

        /// <summary>
        /// Atualizar um determindo usuário.
        /// </summary>
        /// <param name="user">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( UserViewModel user )
        {
            return await ApplicationContext.Users.Where( x => x.Id == user.Id )
                .UpdateAsync( x => new User
                {
                    Active = user.Active ,
                    Name = user.Name ,
                    PasswordHash = user.PasswordHash
                } ).ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}