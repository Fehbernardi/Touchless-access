// =============================================================================
// RepositoryBase.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace Touchless.Access.Services
{
    public class ServiceBase<T>
    {
        #region Propriedades
        /// <summary>
        /// Atribuir/Recuperar objeto responsável por criar um Logger.
        /// </summary>
        protected ILoggerFactory LoggerFactory{ get; set; }

        /// <summary>
        /// Recuperar objeto responsável pelo log das informações.
        /// </summary>
        protected ILogger<T> Logger{ get; }

        /// <summary>
        /// Recuperar objeto contendo as configurações.
        /// </summary>
        protected IConfiguration Configuration{ get; }

        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="configuration">Objeto contendo as configurações da aplicação.</param>
        protected ServiceBase( ILoggerFactory loggerFactory ,IConfiguration configuration )
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException( nameof(loggerFactory) );
            Logger = loggerFactory.CreateLogger<T>();
            Configuration = configuration;
        }
        #endregion
    }
}