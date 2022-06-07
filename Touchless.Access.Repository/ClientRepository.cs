// =============================================================================
// CustomerRepository.cs
// 
// Autor  : Felipe Bernardi
// Data   : 18/05/2022
// =============================================================================

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Touchless.Access.Data;
using Touchless.Access.Data.Models;
using Touchless.Access.Pagination;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Repository
{
    public partial class ClientRepository : RepositoryBase , IClientRepository
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        public ClientRepository( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration ) : base( mapper , applicationContext , configuration )
        {
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
            var newItem = Mapper.Map<Client>( customer );

            await ApplicationContext.Clients.AddAsync( newItem ).ConfigureAwait( false );
            await ApplicationContext.SaveChangesAsync().ConfigureAwait( false );

            return Mapper.Map<ClientViewModel>( newItem );
        }

        /// <summary>
        /// Retornar o(s) cliente(s) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos clientes.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de clientes.</returns>
        public async Task<PagedList<ClientViewModel>> SearchAsync( ClientSearch search , ResourceParameters parameters )
        {
            var items = ApplicationContext.Clients
                .AsNoTracking()
                .AsQueryable();

            #region Aplicar ordenação
            if( string.IsNullOrWhiteSpace( parameters.OrderBy ) ) parameters.OrderBy = "Name,CreatedAt";

            items = items.ApplySort( parameters.OrderBy );
            #endregion

            #region Aplicar filtro
            if( search?.Id != null ) items = items.Where( x => x.Id == search.Id.Value );

            if( search?.Active != null ) items = items.Where( x => x.Active == search.Active.Value );

            if( !string.IsNullOrWhiteSpace( search?.FederalIdentification ) ) items = items.Where( x => x.FederalIdentification == search.FederalIdentification );

            if( !string.IsNullOrWhiteSpace( search?.Name ) )
            {
                var likeExpression = $"%{search.Name}%";
                items = items.Where( x => EF.Functions.ILike( x.Name , likeExpression ) );
            }

            if (search is { IncludeAll: true })
            {
                items = items.Include(x => x.Addresses).Include(x => x.Telephones);
            }
            #endregion
            
            var clients = await PagedList<Client>.CreateAsync(items, parameters.PageNumber, parameters.PageSize).ConfigureAwait(false);

    

            return Mapper.Map<PagedList<ClientViewModel>>(clients);
        }
        
        /// <summary>
        /// Atualizar um determindo cliente.
        /// </summary>
        /// <param name="customer">Objeto contendo as informações do cliente.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( ClientViewModel customer )
        {
            return await ApplicationContext.Clients.Where( x => x.Id == customer.Id )
                .UpdateAsync( x => new Client
                {
                    Active = customer.Active ,
                    FederalIdentification = customer.FederalIdentification ,
                    Name = customer.Name
                } ).ConfigureAwait( false ) > 0;
        }
        #endregion
    }
}