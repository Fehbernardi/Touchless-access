// =============================================================================
// IQueryableApplySortExtension.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Touchless.Access.Pagination
{
    public static class IQueryableApplySortExtension
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Aplicar ordenação na coleção de objetos.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto.</typeparam>
        /// <param name="source">Origem dos objetos.</param>
        /// <param name="sort">String contendo a ordenação desejada.</param>
        /// <returns>Coleção de objetos ordenada.</returns>
        public static IQueryable<T> ApplySort<T>( this IQueryable<T> source , string sort )
        {
            if( source == null ) throw new ArgumentNullException( nameof(source) );

            if( string.IsNullOrEmpty( sort ) ) return source;

            var sortExpression = new StringBuilder();
            var orderByAfterSplit = sort.Split( ',' );
            foreach( var orderByClause in orderByAfterSplit )
            {
                var trimmedOrderByClause = orderByClause.Trim();

                if( trimmedOrderByClause.StartsWith( "-" ) )
                    sortExpression.Append( trimmedOrderByClause.Remove( 0 , 1 ) + " descending," );
                else
                    sortExpression.Append( trimmedOrderByClause + "," );
            }

            if( !string.IsNullOrWhiteSpace( sortExpression.ToString() ) ) source = source.OrderBy( sortExpression.ToString( 0 , sortExpression.Length - 1 ) );
            return source;
        }
        #endregion
    }
}