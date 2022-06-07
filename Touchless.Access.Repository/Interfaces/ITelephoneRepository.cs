// =============================================================================
// ITelephoneRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 17/05/2022
// =============================================================================

using System.Threading.Tasks;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository.Interfaces
{
    public interface ITelephoneRepository
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Remover um telefone.
        /// </summary>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> DeleteAsync( long telephoneId );

        /// <summary>
        /// Atualizar um determinado telefone.
        /// </summary>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAsync( TelephoneViewModel telephone );
        #endregion
    }
}