// =============================================================================
// RolesController.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/01/2022
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

// ReSharper disable RouteTemplates.ControllerRouteParameterIsNotPassedToMethods
// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace Touchless.Access.Services.Api.Controllers
{
    /// <summary>
    /// Responsável pelas operações das funções dos usuários.
    /// </summary>
    [ApiVersion( "1.0" )]
    [ApiController]
    [Produces( "application/json" )]
    [Route( "api/v{version:apiVersion}/roles" )]
    [SwaggerTag( "Responsável pelas operações  das funções dos usuários." )]
    public class RolesController : ApiControllerBase<RolesController>
    {
        #region Variáveis
        private readonly IRoleService _roleService;
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
        /// <param name="roleService">Objeto responsável pelas operações das funções dos usuários.</param>
        public RolesController( ILoggerFactory loggerFactory ,
            IConfiguration configuration ,
            IWebHostEnvironment environment ,
            IMapper mapper ,
            IHttpContextAccessor contextAccessor ,
            IRedisCacheClient cacheRedis ,
            IRoleService roleService ) : base( loggerFactory , configuration , environment , mapper , contextAccessor ,
            cacheRedis )
        {
            _roleService = roleService ?? throw new ArgumentNullException( nameof(roleService) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova função.
        /// </summary>
        /// <param name="request">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( RoleViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status409Conflict , Type = typeof( ConflictError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> AddAsync( [FromBody] RoleViewModel request )
        {
            try
            {
                return Ok( await _roleService.AddAsync( request ).ConfigureAwait( false ) );
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
                return StatusCode( (int)HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Retornar o(s) função(es) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca das funções.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de funções.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( PagedList<RoleViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> SearhAsync( [FromQuery] RoleSearch search , [FromQuery] ResourceParameters parameters )
        {
            try
            {
                var result = await _roleService.SearchAsync( search , parameters ).ConfigureAwait( false );
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
                return StatusCode( (int)HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Atualizar uma determinda função.
        /// </summary>
        /// <param name="roleId">Identificador da função.</param>
        /// <param name="request">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Função não localizada.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPut( "{roleId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> UpdateAsync( [FromRoute] long roleId , [FromBody] RoleViewModel request )
        {
            try
            {
                request.Id = roleId;
                var result = await _roleService.UpdateAsync( request ).ConfigureAwait( false );
                if( result ) return NoContent();

                return NotFound( new NotFoundError( "Função não localizada." ) );
            }
            catch( NotFoundException ex )
            {
                return NotFound( new NotFoundError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int)HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Retornar os usuários associados a função.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Função não localizada.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet( "{roleId:long}/users" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( PagedList<UserViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> GetUsersAsync( [FromRoute] long roleId )
        {
            try
            {
                return Ok( await _roleService.GetUsersAsync( roleId ).ConfigureAwait( false ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int)HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }
        #endregion
    }
}