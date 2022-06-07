// =============================================================================
// CustomerValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using System.Threading.Tasks;
using Touchless.Access.Common;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação dos clientes.
    /// </summary>
    public class ClientValidator : AbstractValidator<ClientViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ClientValidator()
        {
            RuleFor( x => x.Comments ).MaximumLength( 500 ).WhenAsync( ( model , _ ) => Task.FromResult( !string.IsNullOrWhiteSpace( model.Comments ) ) );

            RuleFor( x => x.FederalIdentification ).NotEmpty();
            RuleFor( x => x.FederalIdentification ).Length( 14 );
            RuleFor( x => x.FederalIdentification ).MustAsync( ( s , _ ) => Task.FromResult( Util.IsValidCnpj( s ) ) )
                .WhenAsync( ( model , _ ) => Task.FromResult( !string.IsNullOrWhiteSpace( model.FederalIdentification ) ) );

            RuleFor( x => x.Name ).NotEmpty();
            RuleFor( x => x.Name ).MaximumLength( 255 );
        }
        #endregion
    }
}