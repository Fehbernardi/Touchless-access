// =============================================================================
// AccountsController.cs
// 
// Autor  : Felipe Bernardi
// Data   : 21/01/2022
// =============================================================================
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Touchless.Access.Exception;
using Touchless.Access.Services.Api.Results;
using Touchless.Access.Services.Common.Models;
using IAuthenticationService = Touchless.Access.Services.Interfaces.IAuthenticationService;

namespace Touchless.Access.Services.Api.Controllers
{
    /// <summary>
    /// Responsável pelas operações dos usuários.
    /// </summary>
    [ApiVersion( "1.0" )]
    [ApiController]
    [Produces( "application/json" )]
    [Route( "api/v{version:apiVersion}/accounts" )]
    [SwaggerTag( "Responsável pelas operações dos usuários." )]
    public class AccountsController : ApiControllerBase<AccountsController>
    {
        #region Variáveis
        private readonly IAuthenticationService _authenticationService;
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
        /// <param name="authenticationService">Objeto responsável pelas operações de autenticação.</param>
        public AccountsController( ILoggerFactory loggerFactory ,
            IConfiguration configuration ,
            IWebHostEnvironment environment ,
            IMapper mapper ,
            IHttpContextAccessor contextAccessor ,
            IRedisCacheClient cacheRedis ,
            IAuthenticationService authenticationService ) : base( loggerFactory , configuration , environment , mapper , contextAccessor , cacheRedis )
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException( nameof(authenticationService) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Realizar a mudança da senha.
        /// </summary>
        /// <param name="changePasswordRequestViewModel">Objeto contendo as informações da mudança de senha.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Usuário não localizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPut( "change-password" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized , Type = typeof( UnauthorizedError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> ChangePasswordASync( [FromBody] ChangePasswordRequestViewModel changePasswordRequestViewModel )
        {
            try
            {
                await _authenticationService.ChangePasswordAsync( changePasswordRequestViewModel ).ConfigureAwait( false );
                return NoContent();
            }
            catch( NotFoundException ex )
            {
                return NotFound( new NotFoundError( ex.Message ) );
            }
            catch( UnauthorizedException ex )
            {
                return Unauthorized( new UnauthorizedError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Realizar o login de um determinado usuário.
        /// </summary>
        /// <param name="loginRequestViewModel">Objeto contendo as informações do login.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [AllowAnonymous]
        [HttpPost( "login" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( LoginResultViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized , Type = typeof( UnauthorizedError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> LoginAsync( [FromBody] LoginRequestViewModel loginRequestViewModel )
        {
            try
            {
                return Ok( await _authenticationService.LoginAsync( loginRequestViewModel ).ConfigureAwait( false ) );
            }
            catch( UnauthorizedException ex )
            {
                return Unauthorized( new UnauthorizedError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Realizar o logout do usuário.
        /// </summary>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="401">Não autorizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost( "logout" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized , Type = typeof( UnauthorizedError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _authenticationService.LogoutAsync( User.Identity?.Name ).ConfigureAwait( false );
                return NoContent();
            }
            catch( SecurityTokenException ex )
            {
                return Unauthorized( new UnauthorizedError( ex.Message ) );
            }
            catch( System.Exception ex )
            {
                // Ocorreu um erro não esperado na execução da operação.
                Logger.LogError( ex , "Ocorreu um erro não esperado na execução da operação." );
                return StatusCode( (int) HttpStatusCode.InternalServerError , GetErrorResult( "Ocorreu um erro não esperado na execução da operação." , ex ) );
            }
        }

        /// <summary>
        /// Renovar o token de segurança.
        /// </summary>
        /// <param name="refreshTokenRequestViewModel">Objeto contendo as informações para a renovação do token.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="401">Não autorizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        /// ///
        [AllowAnonymous]
        [HttpPost( "refresh-token" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( RefreshTokenRequestViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status401Unauthorized , Type = typeof( UnauthorizedError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> RefreshTokenAsync( [FromBody] RefreshTokenRequestViewModel refreshTokenRequestViewModel )
        {
            try
            {
                var userName = User.Identity?.Name;
                var accessToken = await HttpContext.GetTokenAsync( "Bearer" , "access_token" );
                var claim = User.FindFirst( ClaimTypes.Role )?.Value ?? string.Empty;

                return Ok( await _authenticationService.RefreshTokenAsync( userName , accessToken , claim , refreshTokenRequestViewModel ).ConfigureAwait( false ) );
            }
            catch( SecurityTokenException ex )
            {
                return Unauthorized( new UnauthorizedError( ex.Message ) );
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