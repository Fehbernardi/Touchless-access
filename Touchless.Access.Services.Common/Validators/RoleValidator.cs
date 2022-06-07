// =============================================================================
// RoleValidator.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using FluentValidation;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Common.Validators
{
    /// <summary>
    /// Objeto responsável pela validação das funções do usuário.
    /// </summary>
    public class RoleValidator : AbstractValidator<RoleViewModel>
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public RoleValidator()
        {
            RuleFor( x => x.Name ).NotEmpty();
            RuleFor( x => x.Name ).MaximumLength( 100 );
        }
        #endregion
    }
}