// =============================================================================
// AddressRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 18/05/2022
// =============================================================================

using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Touchless.Access.Data;
using Touchless.Access.Data.Models;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository
{
    public class AddressRepository : RepositoryBase , IAddressRepository
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        public AddressRepository( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration ) : base( mapper , applicationContext , configuration )
        {
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Remover um endereço da transportadora.
        /// </summary>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteAsync( long addressId )
        {
            return await ApplicationContext.Addresses.Where( x => x.Id == addressId )
                .DeleteAsync()
                .ConfigureAwait( false ) > 0;
        }

        /// <summary>
        /// Atualizar um determinado endereço.
        /// </summary>
        /// <param name="address">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( AddressViewModel address )
        {
            return await ApplicationContext.Addresses.Where( x => x.Id == address.Id )
                .UpdateAsync( x => new Address
                {
                    City = address.City ,
                    Complement = address.Complement ,
                    Number = address.Number ,
                    PostalCode = address.PostalCode ,
                    State = address.State ,
                    Street = address.Street
                } )
                .ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}