// =============================================================================
// PagedList.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/06/2019
// =============================================================================

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Touchless.Access.Pagination
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar a página corrente.
        /// </summary>
        public int CurrentPage{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o tamanho da página.
        /// </summary>
        public int PageSize{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o total de registros.
        /// </summary>
        public int TotalCount{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o total de páginas.
        /// </summary>
        public int TotalPages{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="items">Origem dos dados.</param>
        /// <param name="count">Quantidade de dados.</param>
        /// <param name="pageNumber">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        public PagedList( IEnumerable<T> items , int count , int pageNumber , int pageSize )
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = pageSize == 0 ? 1 : (int) Math.Ceiling( count / (double) pageSize );

            AddRange( items );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Criar a páginação dos dados.
        /// </summary>
        /// <param name="source">Origem dos dados.</param>
        /// <param name="pageNumber">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Página de dados criada.</returns>
        public static IEnumerable<T> Create( IQueryable<T> source , int pageNumber , int pageSize )
        {
            var count = source.Count();
            var items = pageSize == 0 ? source.ToList() : source.Skip( (pageNumber - 1) * pageSize ).Take( pageSize ).ToList();

            return new PagedList<T>( items , count , pageNumber , pageSize );
        }

        /// <summary>
        /// Criar a páginação dos dados.
        /// </summary>
        /// <param name="source">Origem dos dados.</param>
        /// <param name="pageNumber">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Página de dados criada.</returns>
        public static async Task<PagedList<T>> CreateAsync( IQueryable<T> source , int pageNumber , int pageSize )
        {
            var count = await source.CountAsync().ConfigureAwait( false );
            var items = pageSize == 0 ? await source.ToListAsync().ConfigureAwait( false ) : 
                await source.Skip( (pageNumber - 1) * pageSize ).Take( pageSize ).ToListAsync().ConfigureAwait( false );

            return new PagedList<T>( items , count , pageNumber , pageSize );
        }

        /// <summary>
        /// Retornar o cabeçalho da paginação.
        /// </summary>
        /// <returns>String contendo as informações da paginação.</returns>
        public string GetHeaderInformation()
        {
            var paginationMetadata = new
            {
                totalCount = TotalCount ,
                pageSize = PageSize == 0 ? TotalCount : PageSize ,
                currentPage = CurrentPage ,
                totalPages = TotalPages
            };

            return JsonConvert.SerializeObject( paginationMetadata );
        }
        #endregion
    }
}