// =============================================================================
// CustomerRepository.Address.cs
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
    public partial class ClientRepository
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo endereço para o cliente.
        /// </summary>
        /// <param name="clientId">Identificador do cliente.</param>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<AddressViewModel> AddAddressAsync( long clientId , AddressViewModel address )
        {
            var newItem = new Address
            {
                ClientId = clientId ,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await ApplicationContext.Addresses.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<AddressViewModel>( newItem );
        }

        /// <summary>
        /// Remover um endereço do cliente.
        /// </summary>
        /// <param name="ClientId">Identificador do cliente.</param>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteAddressAsync( long ClientId , long addressId )
        {
            return await ApplicationContext.Addresses.Where( x => x.ClientId == ClientId && x.Id == addressId )
                .DeleteAsync()
                .ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}