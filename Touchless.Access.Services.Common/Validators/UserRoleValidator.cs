// =============================================================================
// UserRoleValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação da associação das regras com os usuários.
    /// </summary>
    public class UserRoleValidator : AbstractValidator<UserRoleViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public UserRoleValidator()
        {
            RuleSet( "INSERT" , () =>
            {
                RuleFor( x => x.UserId ).NotEqual( 0 );
            } );

            RuleFor( x => x.RoleId ).NotEqual( 0 );
            
        }
        #endregion
    }
}