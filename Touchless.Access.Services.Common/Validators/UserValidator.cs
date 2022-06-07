// =============================================================================
// UserValidator.cs
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
    /// Objeto responsável pela validação dos usuários.
    /// </summary>
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public UserValidator()
        {
            RuleFor( x => x.Name ).NotEmpty();
            RuleFor( x => x.Name ).MaximumLength( 100 );

            RuleFor( x => x.Username ).NotEmpty();
            RuleFor( x => x.Username ).MinimumLength( 3 );
            RuleFor( x => x.Username ).MaximumLength( 50 );

            RuleSet( "INSERT" , () =>
            {
                RuleFor( x => x.Password ).NotEmpty();
                RuleFor( x => x.Password ).MustAsync( ( x , _ ) => Task.FromResult( PasswordHelper.IsValid( x ) ) );
            } );
        }
        #endregion
    }
}