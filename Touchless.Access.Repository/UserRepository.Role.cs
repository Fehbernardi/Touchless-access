// =============================================================================
// UserRepository.Role.cs
// 
// Autor  : Felipe Bernardi
// Data   : 18/05/2022
// =============================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Touchless.Access.Data.Models;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository
{
    public partial class UserRepository
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova função para o usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns></returns>
        public async Task<UserRoleViewModel> AddUserRoleAync( long userId , long roleId )
        {
            var newItem = new UserRole
            {
                RoleId = roleId ,
                UserId = userId ,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await ApplicationContext.UserRoles.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<UserRoleViewModel>( newItem );
        }

        /// <summary>
        /// Remover uma função do usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteRoleAsync( long userId , long roleId )
        {
            return await ApplicationContext.UserRoles.Where( x => x.UserId == userId && x.RoleId == roleId )
                .DeleteAsync()
                .ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}