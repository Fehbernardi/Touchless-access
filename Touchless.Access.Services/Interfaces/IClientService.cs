// =============================================================================
// IClientService.cs
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
    public interface IClientService
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo endereço para o cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        Task<AddressViewModel> AddAddressAsync( long customerId , AddressViewModel address );

        /// <summary>
        /// Adicionar um novo cliente.
        /// </summary>
        /// <param name="customer">Objeto contendo as informações do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        Task<ClientViewModel> AddAsync( ClientViewModel customer );

        /// <summary>
        /// Adicionar um novo telefone para o cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        Task<TelephoneViewModel> AddTelephoneAsync( long customerId , TelephoneViewModel telephone );

        /// <summary>
        /// Remover um endereço do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> DeleteAddressAsync( long customerId , long addressId );

        /// <summary>
        /// Remover um telefone do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> DeleteTelephoneAsync( long customerId , long telephoneId );

        /// <summary>
        /// Retornar todos os endereços do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <returns>Coleção de bloqueios.</returns>
        Task<List<AddressViewModel>> GetAddressesAsync( long customerId );

        /// <summary>
        /// Retornar todos os telefones do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <returns>Coleção de bloqueios.</returns>
        Task<List<TelephoneViewModel>> GetTelephonesAsync( long customerId );

        /// <summary>
        /// Retornar o(s) cliente(s) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos clientes.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de clientes.</returns>
        Task<PagedList<ClientViewModel>> SearchAsync( ClientSearch search , ResourceParameters parameters );

        /// <summary>
        /// Atualizar um determinado endereço do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAddressAsync( long customerId , AddressViewModel address );

        /// <summary>
        /// Atualizar um determindo cliente.
        /// </summary>
        /// <param name="customer">Objeto contendo as informações do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateAsync( ClientViewModel customer );

        /// <summary>
        /// Atualizar um determinado telefone do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        Task<bool> UpdateTelephoneAsync( long customerId , TelephoneViewModel telephone );
        #endregion
    }
}