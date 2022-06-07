// =============================================================================
// IAddressRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 25/04/2022
// =============================================================================

using System.Threading.Tasks;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository.Interfaces
{
    public interface IAddressRepository
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Remover um endereço.
        /// </summary>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> DeleteAsync( long addressId );

        /// <summary>
        /// Atualizar um determinado endereço.
        /// </summary>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAsync( AddressViewModel address );
        #endregion
    }
}