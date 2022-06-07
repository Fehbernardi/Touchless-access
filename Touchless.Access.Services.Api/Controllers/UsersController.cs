// =============================================================================
// UsersController.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/01/2022
// =============================================================================

using AutoMapper;
using FluentValidation.AspNetCore;
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
    /// Responsável pelas operações dos usuários.
    /// </summary>
    [ApiVersion( "1.0" )]
    [ApiController]
    [Produces( "application/json" )]
    [Route( "api/v{version:apiVersion}/users" )]
    [SwaggerTag( "Responsável pelas operações dos usuários." )]
    public partial class UsersController : ApiControllerBase<UsersController>
    {
        #region Variáveis
        private readonly IUserService _userService;
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
        /// <param name="userService">Objeto responsável pelas operações dos usuários.</param>
        public UsersController( ILoggerFactory loggerFactory ,
            IConfiguration configuration ,
            IWebHostEnvironment environment ,
            IMapper mapper ,
            IHttpContextAccessor contextAccessor ,
            IRedisCacheClient cacheRedis ,
            IUserService userService ) : base( loggerFactory , configuration , environment , mapper , contextAccessor , cacheRedis )
        {
            _userService = userService ?? throw new ArgumentNullException( nameof(userService) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo usuário.
        /// </summary>
        /// <param name="request">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( UserViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status409Conflict , Type = typeof( ConflictError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> AddAsync( [CustomizeValidator( RuleSet = "INSERT" )] [FromBody] UserViewModel request )
        {
            try
            {
                return Ok( await _userService.AddAsync( request ).ConfigureAwait( false ) );
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
        /// Retornar o(s) usuário(es) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos usuários.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de usuários.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( PagedList<UserViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> SearhAsync( [FromQuery] UserSearch search , [FromQuery] ResourceParameters parameters )
        {
            try
            {
                var result = await _userService.SearchAsync( search , parameters ).ConfigureAwait( false );
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
        /// Atualizar um determindo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="request">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Usuário não localizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPut( "{userId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> UpdateAsync( [FromRoute] long userId , [FromBody] UserViewModel request )
        {
            try
            {
                request.Id = userId;
                var result = await _userService.UpdateAsync( request ).ConfigureAwait( false );
                if( result ) return NoContent();

                return NotFound( new NotFoundError( "Usuário não localizado." ) );
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
        #endregion
    }
}