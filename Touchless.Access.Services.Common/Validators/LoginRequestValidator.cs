// =============================================================================
// LoginRequestValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação dos dados de login.
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequestViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public LoginRequestValidator()
        {
            RuleFor( x => x.Password ).NotEmpty();
            RuleFor( x => x.UserName ).NotEmpty();
        }
        #endregion
    }
}