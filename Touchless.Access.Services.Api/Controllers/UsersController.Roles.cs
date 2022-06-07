// =============================================================================
// UsersController.Roles.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/01/2022
// =============================================================================
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Touchless.Access.Exception;
using Touchless.Access.Services.Api.Results;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Api.Controllers
{
    /// <summary>
    /// Responsável pelas operações das funções dos usuários.
    /// </summary>
    public partial class UsersController
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar uma nova função para o usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="request">Objeto contendo as informações da função.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Usuário/Função não localizado(a).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost]
        [Route( "{userId:long}/roles" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( UserRoleViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> AddRoleAsync( [FromRoute] long userId , [CustomizeValidator( RuleSet = "INSERT" )] [FromBody]
            UserRoleViewModel request )
        {
            try
            {
                return Ok( await _userService.AddRoleAsync( userId , request.RoleId ).ConfigureAwait( false ) );
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
        /// Retornar as funções do usuário.
        /// </summary>
        /// <returns>Coleção de funções.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet]
        [Route( "{userId:long}/roles" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( List<RoleViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> ListAsync( [FromRoute] long userId )
        {
            try
            {
                return Ok( await _userService.GetRolesAsync( userId ).ConfigureAwait( false ) );
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
        /// Remover uma função do usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="roleId">Identificador da função.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Usuário/Função não localizada(o).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpDelete]
        [Route( "{userId:long}/roles/{roleId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> DeleteRoleAsync( [FromRoute] long userId , [FromRoute] long roleId )
        {
            try
            {
                if( await _userService.DeleteRoleAsync( userId , roleId ).ConfigureAwait( false ) ) return NoContent();

                return NotFound( new NotFoundError( "Usuário/Função não localizada(o)." ) );
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
        #endregion
    }
}