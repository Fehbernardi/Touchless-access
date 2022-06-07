// =============================================================================
// ClientService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Services.Interfaces;

// ReSharper disable NotAccessedField.Local

namespace Touchless.Access.Services
{
    public partial class ClientService : ServiceBase<ClientService>, IClientService
    {
        #region Variáveis
        private readonly IAddressRepository _addressRepository;
        private readonly IClientRepository _customerRepository;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ITelephoneRepository _telephoneRepository;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="redisCacheClient">Objeto responsável pelo gerenciamento do cache Redis.</param>
        /// <param name="addressRepository">Objeto responsável pelas operações relacionadas aos endereços.</param> 
        /// <param name="customerRepository">Objeto responsável pelas operaçôes relacionadas aos clientes.</param>
        /// <param name="telephoneRepository">Objeto responsável pelas operaçôes relacionadas aos telefones.</param>
        public ClientService( ILoggerFactory loggerFactory ,
            IRedisCacheClient redisCacheClient ,
            IAddressRepository addressRepository ,
            IClientRepository customerRepository ,
            ITelephoneRepository telephoneRepository , 
            IConfiguration configuration ) : base( loggerFactory, configuration )
        {
            _redisCacheClient = redisCacheClient ?? throw new ArgumentNullException( nameof(redisCacheClient) );
            _addressRepository = addressRepository ?? throw new ArgumentNullException( nameof(addressRepository) );
            _customerRepository = customerRepository ?? throw new ArgumentNullException( nameof(customerRepository) );
            _telephoneRepository = telephoneRepository ?? throw new ArgumentNullException( nameof(telephoneRepository) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo cliente.
        /// </summary>
        /// <param name="customer">Objeto contendo as informações do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<ClientViewModel> AddAsync( ClientViewModel customer )
        {
            var carriers = await _customerRepository.SearchAsync(
                    new ClientSearch
                    {
                        FederalIdentification = customer.FederalIdentification
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( carriers.Any() ) throw new DuplicateResourceException( "CNPJ" , "Já existe um cliente cadastrado com esse CNPJ." );

            customer.CreatedAt = DateTimeOffset.UtcNow;
            return await _customerRepository.AddAsync( customer ).ConfigureAwait( false );
        }

        /// <summary>
        /// Retornar o(s) cliente(s) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos clientes.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de clientes.</returns>
        public async Task<PagedList<ClientViewModel>> SearchAsync( ClientSearch search , ResourceParameters parameters )
        {
            return await _customerRepository.SearchAsync( search , parameters ).ConfigureAwait( false );
        }

        /// <summary>
        /// Atualizar um determindo cliente.
        /// </summary>
        /// <param name="customer">Objeto contendo as informações do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( ClientViewModel customer )
        {
            var carriers = await _customerRepository.SearchAsync(
                    new ClientSearch
                    {
                        Id = customer.Id
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !carriers.Any() ) throw new NotFoundException( "Cliente não localizado." );

            return await _customerRepository.UpdateAsync( customer ).ConfigureAwait( false );
        }
        #endregion
    }
}