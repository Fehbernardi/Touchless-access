// =============================================================================
// clientsController.Telephone.cs
// 
// Autor  : Felipe Bernardi
// Data   : 25/01/2022
// =============================================================================
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Touchless.Access.Exception;
using Touchless.Access.Services.Api.Results;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Api.Controllers
{
    /// <summary>
    /// Responsável pelas operações dos telefones dos clientes.
    /// </summary>
    public partial class ClientController
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo telefone para o cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="request">Objeto contendo as informações do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Cliente não localizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost]
        [Route( "{customerId:long}/telephones" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( TelephoneViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> AddTelephoneAsync( [FromRoute] long customerId , [FromBody] TelephoneViewModel request )
        {
            try
            {
                return Ok( await _clientService.AddTelephoneAsync( customerId , request ).ConfigureAwait( false ) );
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
        /// Remover um telefone do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Cliente/Telefone não localizada(o).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpDelete]
        [Route( "{customerId:long}/telephones/{telephoneId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> DeleteTelephoneAsync( [FromRoute] long customerId , [FromRoute] long telephoneId )
        {
            try
            {
                if( await _clientService.DeleteTelephoneAsync( customerId , telephoneId ).ConfigureAwait( false ) ) return NoContent();

                return NotFound( new NotFoundError( "Cliente/Telefone não localizada(o)." ) );
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
        /// Retornar os telefones do cliente.
        /// </summary>
        /// <param name="customerId">Identificador da cliente.</param>
        /// <returns>Coleção de telefones.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet]
        [Route( "{customerId:long}/telephones" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( List<TelephoneViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> ListTelephonesAsync( [FromRoute] long customerId )
        {
            try
            {
                return Ok( await _clientService.GetTelephonesAsync( customerId ).ConfigureAwait( false ) );
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
        /// Adicionar um determinado telefone do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="telephoneId">Identificador do telefone.</param>
        /// <param name="request">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Cliente/Telefone não localizada(o).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPut]
        [Route( "{customerId:long}/telephones/{telephoneId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> UpdateTelephoneAsync( [FromRoute] long customerId , [FromRoute] long telephoneId , [FromBody] TelephoneViewModel request )
        {
            try
            {
                request.Id = telephoneId;
                var result = await _clientService.UpdateTelephoneAsync( customerId , request ).ConfigureAwait( false );
                if( result ) return NoContent();

                return NotFound( new NotFoundError( "Cliente/Telefone não localizada(o)." ) );
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