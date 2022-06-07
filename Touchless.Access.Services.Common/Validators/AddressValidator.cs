// =============================================================================
// AddressValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using System.Threading.Tasks;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação dos endereços.
    /// </summary>
    public class AddressValidator : AbstractValidator<AddressViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public AddressValidator()
        {
            RuleFor( x => x.City ).NotEmpty();
            RuleFor( x => x.City ).MaximumLength( 255 );

            RuleFor( x => x.Complement ).MaximumLength( 255 ).WhenAsync( ( model , _ ) => Task.FromResult( !string.IsNullOrWhiteSpace( model.Complement ) ) );

            RuleFor( x => x.Label ).NotEmpty();
            RuleFor( x => x.Label ).MaximumLength( 255 );

            RuleFor( x => x.PostalCode ).NotEmpty();
            RuleFor( x => x.PostalCode ).MaximumLength( 20 );

            RuleFor( x => x.State ).NotEmpty();
            RuleFor( x => x.State ).Length( 2 );

            RuleFor( x => x.Street ).NotEmpty();
            RuleFor( x => x.Street ).MaximumLength( 255 );
        }
        #endregion
    }
}