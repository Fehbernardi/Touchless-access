// =============================================================================
// AuthenticationKey.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/05/2022
// =============================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Touchless.Access.Data.Models
{
    /// <summary>
    /// Objeto representando a tabela AuthenticationKey.
    /// </summary>
    [Table( "authentication_key" )]
    public sealed class AuthenticationKey
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se a chave esta ativa ou não.
        /// </summary>
        [Required]
        [Column( "active" )]
        public bool Active{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o código.
        /// </summary>
        [Required]
        [StringLength( 100 )]
        [Column( "key" )]
        public string Code{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a data de criação.
        /// </summary>
        [Required]
        [Column( "created_at" )]
        public DateTimeOffset CreatedAt{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o identificador do registro.
        /// </summary>
        [Key]
        [Column( "id" )]
        public long Id{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o rótulo.
        /// </summary>
        [Required]
        [StringLength( 100 )]
        [Column( "label" )]
        public string Label{ get; set; }
        #endregion
    }
}