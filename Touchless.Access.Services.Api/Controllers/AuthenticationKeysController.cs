// =============================================================================
// AuthenticationKeysController.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/08/2022
// =============================================================================
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Api.Results;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Services.Interfaces;

namespace Touchless.Access.Services.Api.Controllers
{
    /// <summary>
    /// Responsável pelas operações das chaves de autenticação.
    /// </summary>
    [ApiVersion( "1.0" )]
    [ApiController]
    [Produces( "application/json" )]
    [Route( "api/v{version:apiVersion}/authentication-keys" )]
    [SwaggerTag( "Responsável pelas operações das chaves de autenticação." )]
    public class AuthenticationKeysController : ApiControllerBase<AuthenticationKeysController>
    {
        #region Variáveis
        private readonly IAuthenticationKeyService _authenticationKeyService;
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
        /// <param name="authenticationKeyService">Objeto responsável pelas operações das chaves de autenticação.</param>
        public AuthenticationKeysController( ILoggerFactory loggerFactory ,
            IConfiguration configuration ,
            IWebHostEnvironment environment ,
            IMapper mapper ,
            IHttpContextAccessor contextAccessor ,
            IRedisCacheClient cacheRedis ,
            IAuthenticationKeyService authenticationKeyService ) : base( loggerFactory , configuration , environment , mapper , contextAccessor , cacheRedis )
        {
            _authenticationKeyService = authenticationKeyService ?? throw new ArgumentNullException( nameof(authenticationKeyService) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova chave de autenticação.
        /// </summary>
        /// <param name="request">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( AuthenticationKeyViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status409Conflict , Type = typeof( ConflictError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> AddAsync( [FromBody] AuthenticationKeyViewModel request )
        {
            try
            {
                return Ok( await _authenticationKeyService.AddAsync( request ).ConfigureAwait( false ) );
            }
            catch( NotFoundException ex )
            {
                return NotFound( new NotFoundError( ex.Message ) );
            }
            catch( DuplicateResourceException ex )
            {
                return Conflict( new ConflictError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Remover uma chave de autenticação.
        /// </summary>
        /// <param name="authenticationKeyId">Identificador da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Chave de autenticação não localizada.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpDelete( "{authenticationKeyId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> DeleteAsync( long authenticationKeyId )
        {
            try
            {
                var result = await _authenticationKeyService.DeleteAsync( authenticationKeyId ).ConfigureAwait( false );
                if( result ) return NoContent();

                return NotFound( new NotFoundError( "Chave de autenticação não localizada." ) );
            }
            catch( NotFoundException ex )
            {
                return NotFound( new NotFoundError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Retornar a(s) chave(s) de autenticação que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das chaves de autenticação.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de chaves de autenticação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( PagedList<AuthenticationKeyViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> SearhAsync( [FromQuery] AuthenticationKeySearch search , [FromQuery] ResourceParameters parameters )
        {
            try
            {
                var result = await _authenticationKeyService.SearchAsync( search , parameters ).ConfigureAwait( false );
                AddPaginationHeader( result );
                return Ok( result );
            }
            catch( NotFoundException ex )
            {
                return NotFound( new NotFoundError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Atualizar uma determinda chave de autenticação.
        /// </summary>
        /// <param name="authenticationKeyId">Identificador da chave de autenticação.</param>
        /// <param name="request">Objeto contendo as informações da chave de autenticação.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Adaptador não localizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPut( "{authenticationKeyId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> UpdateAsync( [FromRoute] long authenticationKeyId , [FromBody] AuthenticationKeyViewModel request )
        {
            try
            {
                request.Id = authenticationKeyId;
                var result = await _authenticationKeyService.UpdateAsync( request ).ConfigureAwait( false );
                if( result ) return NoContent();

                return NotFound( new NotFoundError( "Adaptador não localizada." ) );
            }
            catch( NotFoundException ex )
            {
                return NotFound( new NotFoundError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }
        #endregion
    }
}