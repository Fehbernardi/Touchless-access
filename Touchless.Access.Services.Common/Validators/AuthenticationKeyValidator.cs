// =============================================================================
// AuthenticationKeyValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação das chaves de autenticação.
    /// </summary>
    public class AuthenticationKeyValidator : AbstractValidator<AuthenticationKeyViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public AuthenticationKeyValidator()
        {
            RuleSet( "INSERT" , () =>
            {
                RuleFor( x => x.Code ).NotEmpty();
                RuleFor( x => x.Code ).MaximumLength( 100 );
            } );

            RuleFor( x => x.Label ).NotEmpty();
            RuleFor( x => x.Label ).MaximumLength( 100 );
        }
        #endregion
    }
}