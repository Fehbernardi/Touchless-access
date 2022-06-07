// =============================================================================
// TelephoneValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação dos telefones.
    /// </summary>
    public class TelephoneValidator : AbstractValidator<TelephoneViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public TelephoneValidator()
        {
            RuleFor( x => x.CountryCode ).NotEmpty();
            RuleFor( x => x.CountryCode ).MaximumLength( 5 );

            RuleFor( x => x.Number ).NotEmpty();
            RuleFor( x => x.Number ).MaximumLength( 20 );

            RuleFor( x => x.RegionCode ).NotEmpty();
            RuleFor( x => x.RegionCode ).MaximumLength( 5 );
        }
        #endregion
    }
}