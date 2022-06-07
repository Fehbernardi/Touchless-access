// =============================================================================
// IRoleService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Interfaces
{
    public interface IRoleService
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova função.
        /// </summary>
        /// <param name="role">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        Task<RoleViewModel> AddAsync( RoleViewModel role );

        /// <summary>
        /// Retornar os usuários associados a função.
        /// </summary>
        /// <param name="roleId">Identificador único da função.</param>
        /// <returns>Resultado da operação.</returns>
        Task<List<UserViewModel>> GetUsersAsync( long roleId );

        /// <summary>
        /// Retornar o(s) função(es) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das funções.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de funções.</returns>
        Task<PagedList<RoleViewModel>> SearchAsync( RoleSearch search , ResourceParameters parameters );

        /// <summary>
        /// Atualizar uma determinda função.
        /// </summary>
        /// <param name="role">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAsync( RoleViewModel role );
        #endregion
    }
}