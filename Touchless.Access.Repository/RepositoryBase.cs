// =============================================================================
// RepositoryBase.cs
// 
// Autor  : Felipe Bernardi
// Data   : 18/05/2022
// =============================================================================

using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using Touchless.Access.Data;

namespace Touchless.Access.Repository
{
    public class RepositoryBase
    {
        #region Propriedades
        /// <summary>
        /// Recuperar objeto contendo o contexto de acesso ao banco de dados.
        /// </summary>
        protected ApplicationContext ApplicationContext{ get; }

        /// <summary>
        /// Recuperar objeto contendo as configurações.
        /// </summary>
        protected IConfiguration Configuration{ get; }

        /// <summary>
        /// Recuperar objeto responsável pelo mapeamento dos objetos.
        /// </summary>
        protected IMapper Mapper{ get; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="applicationContext">Objeto contendo o contexto de acesso ao banco de dados.</param>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        protected RepositoryBase( IMapper mapper , ApplicationContext applicationContext , IConfiguration configuration )
        {
            Mapper = mapper ?? throw new ArgumentNullException( nameof(mapper) );
            ApplicationContext = applicationContext ?? throw new ArgumentNullException( nameof(applicationContext) );
            Configuration = configuration;
        }
        #endregion
    }
}