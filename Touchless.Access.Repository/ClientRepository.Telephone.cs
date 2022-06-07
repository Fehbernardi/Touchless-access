// =============================================================================
// CustomerRepository.Telephone.cs
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
        /// Adicionar um novo telefone para o cliente.
        /// </summary>
        /// <param name="clientId">Identificador do cliente.</param>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<TelephoneViewModel> AddTelephoneAsync( long clientId , TelephoneViewModel telephone )
        {
            var newItem = new Telephone
            {
                ClientId = clientId ,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await ApplicationContext.Telephones.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<TelephoneViewModel>( newItem );
        }

        /// <summary>
        /// Remover um telefone do cliente.
        /// </summary>
        /// <param name="ClientId">Identificador do cliente.</param>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteTelephoneAsync( long ClientId , long telephoneId )
        {
            return await ApplicationContext.Telephones.Where( x => x.ClientId == ClientId && x.Id == telephoneId )
                .DeleteAsync()
                .ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}