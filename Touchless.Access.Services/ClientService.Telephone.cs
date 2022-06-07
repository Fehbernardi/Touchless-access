// =============================================================================
// ClientService.Telephone.cs
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
        /// Adicionar um novo telefone para o cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<TelephoneViewModel> AddTelephoneAsync( long customerId , TelephoneViewModel telephone )
        {
            var clients = await _customerRepository.SearchAsync(
                    new ClientSearch
                    {
                        Id = customerId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !clients.Any() ) throw new NotFoundException( "Cliente não localizado." );

            return await _customerRepository.AddTelephoneAsync( customerId , telephone ).ConfigureAwait( false );
        }

        /// <summary>
        /// Remover um telefone do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteTelephoneAsync( long customerId , long telephoneId )
        {
            using var transactionScope = new TransactionScope( TransactionScopeOption.RequiresNew , TransactionScopeAsyncFlowOption.Enabled );
            var result1 = await _customerRepository.DeleteTelephoneAsync( customerId , telephoneId ).ConfigureAwait( false );
            var result2 = await _telephoneRepository.DeleteAsync( telephoneId ).ConfigureAwait( false );

            transactionScope.Complete();
            return result1 && result2;
        }

        /// <summary>
        /// Retornar todos os telefones do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <returns>Coleção de bloqueios.</returns>
        public async Task<List<TelephoneViewModel>> GetTelephonesAsync( long customerId )
        {
            var clients = await _customerRepository.SearchAsync( new ClientSearch {Id = customerId} , new ResourceParameters() ).ConfigureAwait( false );
            var result = new List<TelephoneViewModel>();
            if( !clients.Any() ) return result;

            var customerTelephones = clients.First().ClientsTelephones.ToList();
            result.AddRange( from customerTelephoneViewModel in customerTelephones select customerTelephoneViewModel.Telephone );
            return result;
        }

        /// <summary>
        /// Atualizar um determinado telefone do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateTelephoneAsync( long customerId , TelephoneViewModel telephone )
        {
            var clients = await _customerRepository.SearchAsync(
                    new ClientSearch
                    {
                        Id = customerId
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !clients.Any() ) throw new NotFoundException( "Cliente não localizado." );

            return await _telephoneRepository.UpdateAsync( telephone ).ConfigureAwait( false );
        }
        #endregion
    }
}