// =============================================================================
// LastAccess.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/05/2022
// =============================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Touchless.Access.Data.Models
{
    [Table( "last_access" )]
    public sealed class LastAccess
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do cliente.
        /// </summary>
        public Client Client{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador da transportadora.
        /// </summary>
        [Required]
        [Column( "client_id" )]
        public long ClientId{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a data de criação.
        /// </summary>
        [Required]
        [Column( "entry_time" )]
        public DateTimeOffset EntryTime{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o identificador do registro.
        /// </summary>
        [Key]
        [Column( "id" )]
        public long Id{ get; set; }
        #endregion
    }
}