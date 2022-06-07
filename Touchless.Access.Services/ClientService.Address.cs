// =============================================================================
// ClientService.Address.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services
{
    public partial class ClientService
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo endereço para o cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<AddressViewModel> AddAddressAsync( long customerId , AddressViewModel address )
        {
            var clients = await _customerRepository.SearchAsync(
                    new ClientSearch
                    {
                        Id = customerId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !clients.Any() ) throw new NotFoundException( "Cliente não localizado." );

            return await _customerRepository.AddAddressAsync( customerId , address ).ConfigureAwait( false );
        }

        /// <summary>
        /// Remover um endereço do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteAddressAsync( long customerId , long addressId )
        {
            using var transactionScope = new TransactionScope( TransactionScopeOption.RequiresNew , TransactionScopeAsyncFlowOption.Enabled );
            var result1 = await _customerRepository.DeleteAddressAsync( customerId , addressId ).ConfigureAwait( false );
            var result2 = await _addressRepository.DeleteAsync( addressId ).ConfigureAwait( false );

            transactionScope.Complete();
            return result1 && result2;
        }

        /// <summary>
        /// Retornar todos os endereços do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <returns>Coleção de bloqueios.</returns>
        public async Task<List<AddressViewModel>> GetAddressesAsync( long customerId )
        {
            var clients = await _customerRepository.SearchAsync( new ClientSearch {Id = customerId} , new ResourceParameters() ).ConfigureAwait( false );
            var result = new List<AddressViewModel>();
            if( !clients.Any() ) return result;

            var customerAddresses = clients.First().ClientAddresses.ToList();
            result.AddRange( from customerAddressViewModel in customerAddresses select customerAddressViewModel.Address );
            return result;
        }

        /// <summary>
        /// Atualizar um determinado endereço do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAddressAsync( long customerId , AddressViewModel address )
        {
            var clients = await _customerRepository.SearchAsync(
                    new ClientSearch
                    {
                        Id = customerId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !clients.Any() ) throw new NotFoundException( "Cliente não localizado." );

            return await _addressRepository.UpdateAsync( address ).ConfigureAwait( false );
        }
        #endregion
    }
}