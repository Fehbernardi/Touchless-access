// =============================================================================
// UserService.Role.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services
{
    public partial class UserService
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova função para o usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<UserRoleViewModel> AddRoleAsync( long userId , long roleId )
        {
            var users = await _userRepository.SearchAsync(
                    new UserSearch
                    {
                        Id = userId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !users.Any() ) throw new NotFoundException( "Usuário não localizado." );

            var roles = await _roleRepository.SearchAsync(
                    new RoleSearch
                    {
                        Id = roleId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !roles.Any() ) throw new NotFoundException( "Função não localizada." );

            return await _userRepository.AddUserRoleAync( userId , roleId ).ConfigureAwait( false );
        }

        /// <summary>
        /// Remover uma função do usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteRoleAsync( long userId , long roleId )
        {
            return await _userRepository.DeleteRoleAsync( userId , roleId ).ConfigureAwait( false );
        }

        /// <summary>
        /// Retornar todas as funções do usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Coleção de funções.</returns>
        public async Task<List<RoleViewModel>> GetRolesAsync( long userId )
        {
            var users = await _userRepository.SearchAsync( new UserSearch {Id = userId} , new ResourceParameters() ).ConfigureAwait( false );
            var result = new List<RoleViewModel>();
            if( !users.Any() ) return result;

            var userRoles = users.First().UserRoles.ToList();
            result.AddRange( from userRoleViewModel in userRoles select userRoleViewModel.Role );
            return result;
        }

        /// <summary>
        /// Retornar todas as funções do usuário.
        /// </summary>
        /// <param name="username">Usuário.</param>
        /// <returns>Coleção de funções.</returns>
        public async Task<List<RoleViewModel>> GetRolesAsync( string username )
        {
            var users = await _userRepository.SearchAsync( new UserSearch {Username = username} , new ResourceParameters() ).ConfigureAwait( false );
            var result = new List<RoleViewModel>();
            if( !users.Any() ) return result;

            var userRoles = users.First().UserRoles.ToList();
            result.AddRange( from userRoleViewModel in userRoles select userRoleViewModel.Role );
            return result;
        }
        #endregion
    }
}