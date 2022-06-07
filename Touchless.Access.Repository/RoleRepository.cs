// =============================================================================
// RoleRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 18/05/2022
// =============================================================================

using System.Collections.Generic;
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
    public class RoleRepository : RepositoryBase , IRoleRepository
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        public RoleRepository( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration ) : base( mapper , applicationContext , configuration )
        {
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
            var newItem = Mapper.Map<Role>( role );

            await ApplicationContext.Roles.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<RoleViewModel>( newItem );
        }

        /// <summary>
        /// Retornar os usuários associados a função.
        /// </summary>
        /// <param name="roleId">Identificador único da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<List<UserViewModel>> GetUsersAsync( long roleId )
        {
            var userRoles = await ApplicationContext.UserRoles
                .Include( x => x.User )
                .Where( x => x.RoleId == roleId )
                .ToListAsync()
                .ConfigureAwait( false );

            return (from userRole in userRoles select Mapper.Map<UserViewModel>( userRole.User )).ToList();
        }

        /// <summary>
        /// Retornar o(s) função(es) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das funções.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de funções.</returns>
        public async Task<PagedList<RoleViewModel>> SearchAsync( RoleSearch search , ResourceParameters parameters )
        {
            var roles = ApplicationContext.Roles
                .AsNoTracking()
                .AsQueryable();

            #region Aplicar ordenação
            if( string.IsNullOrWhiteSpace( parameters.OrderBy ) ) parameters.OrderBy = "Name,CreatedAt";

            roles = roles.ApplySort( parameters.OrderBy );
            #endregion

            #region Aplicar filtro
            if( search?.Id != null ) roles = roles.Where( x => x.Id == search.Id.Value );

            if( !string.IsNullOrWhiteSpace( search?.Name ) )
            {
                var likeExpression = $"%{search.Name}%";
                roles = roles.Where( x => EF.Functions.ILike( x.Name , likeExpression ) );
            }
            #endregion

            return Mapper.Map<PagedList<RoleViewModel>>( await PagedList<Role>.CreateAsync( roles , parameters.PageNumber , parameters.PageSize ).ConfigureAwait( false ) );
        }

        /// <summary>
        /// Atualizar uma determinda função.
        /// </summary>
        /// <param name="role">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( RoleViewModel role )
        {
            return await ApplicationContext.Roles.Where( x => x.Id == role.Id )
                .UpdateAsync( x => new Role
                {
                    Name = role.Name
                } ).ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}