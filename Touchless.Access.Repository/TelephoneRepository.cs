// =============================================================================
// TelephoneRepository.cs
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
    public class TelephoneRepository : RepositoryBase , ITelephoneRepository
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        public TelephoneRepository( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration ) : base( mapper , applicationContext , configuration )
        {
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Remover um telefone.
        /// </summary>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> DeleteAsync( long telephoneId )
        {
            return await ApplicationContext.Telephones.Where( x => x.Id == telephoneId )
                .DeleteAsync()
                .ConfigureAwait( false ) > 0;
        }

        /// <summary>
        /// Atualizar um determinado telefone.
        /// </summary>
        /// <param name="telephone">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( TelephoneViewModel telephone )
        {
            return await ApplicationContext.Telephones.Where( x => x.Id == telephone.Id )
                .UpdateAsync( x => new Telephone
                {
                    CountryCode = telephone.CountryCode ,
                    Number = telephone.Number ,
                    RegionCode = telephone.RegionCode
                } )
                .ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}