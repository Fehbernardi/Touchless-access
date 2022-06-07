// =============================================================================
// IUserRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 17/05/2022
// =============================================================================

using System.Threading.Tasks;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository.Interfaces
{
    public interface IUserRepository
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo usuário.
        /// </summary>
        /// <param name="user">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        Task<UserViewModel> AddAsync( UserViewModel user );

        /// <summary>
        /// Adicionar uma nova função para o usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns></returns>
        Task<UserRoleViewModel> AddUserRoleAync( long userId , long roleId );

        /// <summary>
        /// Remover uma função do usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> DeleteRoleAsync( long userId , long roleId );

        /// <summary>
        /// Retornar o(s) usuário(s) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos usuários.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de usuários.</returns>
        Task<PagedList<UserViewModel>> SearchAsync( UserSearch search , ResourceParameters parameters );

        /// <summary>
        /// Atualizar um determindo usuário.
        /// </summary>
        /// <param name="user">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAsync( UserViewModel user );
        #endregion
    }
}