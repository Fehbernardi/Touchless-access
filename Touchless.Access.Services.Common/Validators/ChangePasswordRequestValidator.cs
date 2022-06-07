// =============================================================================
// ChangePasswordRequestValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using System.Threading.Tasks;
using Touchless.Access.Common.Helpers;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação dos dados de login.
    /// </summary>
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ChangePasswordRequestValidator()
        {
            RuleFor( x => x.OldPassword ).NotEmpty();

            RuleFor( x => x.NewPassword ).NotEmpty();
            RuleFor( x => x.NewPassword ).MinimumLength( 6 );
            RuleFor( x => x.NewPassword ).MaximumLength( 15 );
            RuleFor( x => x.NewPassword ).MustAsync( ( s , _ ) => Task.FromResult( PasswordHelper.IsValid( s ) ) );
        }
        #endregion
    }
}