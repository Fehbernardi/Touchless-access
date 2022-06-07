// =============================================================================
// BaseViewModel.cs
// 
// Autor  : Felipe Bernardi
// Data   : 27/04/2022
// =============================================================================

using System;

namespace Touchless.Access.Services.Common.Models
{
    /// <summary>
    /// Objeto contendo as propriedades padrões de todos os objetos.
    /// </summary>
    public class BaseViewModel
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar a data de criação.
        /// </summary>
        public DateTimeOffset CreatedAt{ get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Atribuir/Recuperar o identificador do registro.
        /// </summary>
        public long Id{ get; set; }
        #endregion
    }
}