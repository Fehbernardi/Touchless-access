// =============================================================================
// clientsController.Address.cs
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
    /// Responsável pelas operações dos endereços dos clientes.
    /// </summary>
    public partial class ClientController
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo endereço para o cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="request">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Cliente não localizado.</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPost]
        [Route( "{customerId:long}/addresses" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( AddressViewModel ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> AddAddressAsync( [FromRoute] long customerId , [FromBody] AddressViewModel request )
        {
            try
            {
                return Ok( await _clientService.AddAddressAsync( customerId , request ).ConfigureAwait( false ) );
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
        /// Remover um endereço do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Cliente/Endereço não localizada(o).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpDelete]
        [Route( "{customerId:long}/addresses/{addressId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> DeleteAddressAsync( [FromRoute] long customerId , [FromRoute] long addressId )
        {
            try
            {
                if( await _clientService.DeleteAddressAsync( customerId , addressId ).ConfigureAwait( false ) ) return NoContent();

                return NotFound( new NotFoundError( "Cliente/Endereço não localizada(o)." ) );
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
        /// Retornar os endereços do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <returns>Coleção de endereços.</returns>
        /// <response code="200">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpGet]
        [Route( "{customerId:long}/addresses" )]
        [ProducesResponseType( StatusCodes.Status200OK , Type = typeof( List<AddressViewModel> ) )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> ListAddressesAsync( [FromRoute] long customerId )
        {
            try
            {
                return Ok( await _clientService.GetAddressesAsync( customerId ).ConfigureAwait( false ) );
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
        /// Atualizar um determinado endereço do cliente.
        /// </summary>
        /// <param name="customerId">Identificador do cliente.</param>
        /// <param name="addressId">Identificador do endereço.</param>
        /// <param name="request">Objeto contendo as informações do endereço.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Resultado da operação.</response>
        /// <response code="400">Parâmetro(s) inválido(s).</response>
        /// <response code="404">Cliente/Endereço não localizada(o).</response>
        /// <response code="500">Ocorreu um erro não esperado na execução da operação.</response>
        [HttpPut]
        [Route( "{customerId:long}/addresses/{addressId:long}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status400BadRequest , Type = typeof( BadRequestError ) )]
        [ProducesResponseType( StatusCodes.Status404NotFound , Type = typeof( NotFoundError ) )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError , Type = typeof( GenericError ) )]
        public async Task<IActionResult> UpdateAddressAsync( [FromRoute] long customerId , [FromRoute] long addressId , [FromBody] AddressViewModel request )
        {
            try
            {
                request.Id = addressId;
                var result = await _clientService.UpdateAddressAsync( customerId , request ).ConfigureAwait( false );
                if( result ) return NoContent();

                return NotFound( new NotFoundError( "Cliente/Endereço não localizada(o)." ) );
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