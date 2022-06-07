// =============================================================================
// ApiControllerBase.cs
// 
// Autor  : Felipe Bernardi
// Data   : 17/05/2022
// =============================================================================

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Api.Results;
namespace Touchless.Access.Services.Api.Controllers
{
    /// <summary>
    /// </summary>
    [Authorize]
    [EnableCors( "PolicyAccess" )]
    public class ApiControllerBase<T> : ControllerBase
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
        /// Atribuir/Recuperar o objeto contendo as configurações.
        /// </summary>
        internal IConfiguration Configuration{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar objeto responsável pelo acesso a requisicao HTTP.
        /// </summary>
        internal IHttpContextAccessor ContextAccessor{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o objeto contendo as informações do ambiente.
        /// </summary>
        internal IWebHostEnvironment Environment{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar objeto responsável pelo mapeamento dos objetos.
        /// </summary>
        internal IMapper Mapper{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="loggerFactory">Objeto responsável por criar um Logger.</param>
        /// <param name="configuration">Objeto contendo as configurações.</param>
        /// <param name="environment">Objeto contendo as informações do ambiente.</param>
        /// <param name="mapper">Objeto responsável pelo mapeamento dos objetos.</param>
        /// <param name="contextAccessor">Objeto responsável pelo acesso a requisicao HTTP.</param>
        /// <param name="cacheRedis">Objeto responsável pelo gerenciamento do cache Redis.</param>
        public ApiControllerBase( ILoggerFactory loggerFactory ,
            IConfiguration configuration ,
            IWebHostEnvironment environment ,
            IMapper mapper ,
            IHttpContextAccessor contextAccessor ,
            IRedisCacheClient cacheRedis )
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException( nameof(loggerFactory) );
            Logger = loggerFactory.CreateLogger<T>();

            Configuration = configuration ?? throw new ArgumentNullException( nameof(configuration) );
            Environment = environment ?? throw new ArgumentNullException( nameof(environment) );
            Mapper = mapper ?? throw new ArgumentNullException( nameof(mapper) );
            ContextAccessor = contextAccessor ?? throw new ArgumentNullException( nameof(contextAccessor) );
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Retornar o objeto <see cref="GenericError" /> contendo o detalhamento do erro.
        /// </summary>
        /// <param name="message">Mensagem de descrição do erro.</param>
        /// <param name="exception">Objeto contendo as informações da exceção.</param>
        /// <returns>Objeto <see cref="GenericError" /> contendo o detalhamento do erro.</returns>
        internal static GenericError GetErrorResult( string message , System.Exception exception = null )
        {
            var result = new GenericError { Message = message };

            if( exception == null ) return result;

            result.StackTrace = exception.StackTrace;

            var innerException = exception.InnerException;

            while( innerException != null )
            {
                result.InnerError ??= new List<GenericError>();

                if( !string.IsNullOrEmpty( innerException.Message ) ) result.InnerError.Add( new GenericError { Message = innerException.Message } );

                innerException = innerException.InnerException;

                if( innerException != null ) result.InnerError.Add( new GenericError { Message = innerException.Message } );
            }

            return result;
        }

        /// <summary>
        /// Criar o resultado com os dados paginado da solicitação.
        /// </summary>
        /// <typeparam name="O">Tipo do objeto.</typeparam>
        /// <param name="value">Coleção contendo os dados de forma paginada.</param>
        /// <returns>Mensagem de resposta.</returns>
        internal void AddPaginationHeader<O>( PagedList<O> value )
        {
            Response.Headers.Add( "X-Pagination" , value.GetHeaderInformation() );
        }
        #endregion
    }
}