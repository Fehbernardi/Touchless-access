// =============================================================================
// PagedListConverter.cs
// 
// Autor  : Felipe Bernardi
// Data   : 06/06/2022
// =============================================================================

using AutoMapper;
using System.Linq;

namespace Touchless.Access.Pagination
{
    /// <summary>
    /// Classe responsável pela conversão do objeto <see cref="PagedList{T}" />
    /// </summary>
    /// <typeparam name="TIn">Tipo do objeto de origem.</typeparam>
    /// <typeparam name="TOut">Tipo do objeto de destino.</typeparam>
    public class PagedListConverter<TIn , TOut> : ITypeConverter<PagedList<TIn> , PagedList<TOut>>
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Realizar a conversão.
        /// </summary>
        /// <param name="source">Objeto de origem.</param>
        /// <param name="destination">Objeto de destino.</param>
        /// <param name="context">Contexto da conversão.</param>
        /// <returns>Objeto resultante da conversão.</returns>
        public PagedList<TOut> Convert( PagedList<TIn> source , PagedList<TOut> destination , ResolutionContext context )
        {
            var viewModels = source.Select( context.Mapper.Map<TIn , TOut> ).ToList();
            return new PagedList<TOut>( viewModels , source.TotalCount , source.CurrentPage , source.PageSize );
        }
        #endregion
    }
}