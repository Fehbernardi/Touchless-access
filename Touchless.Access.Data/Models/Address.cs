// =============================================================================
// Address.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/05/2022
// =============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Touchless.Access.Data.Models
{
    /// <summary>
    /// Objeto representando a tabela Address.
    /// </summary>
    [Table( "address" )]
    public sealed class Address
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar Objeto do cliente.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador unico do cliente.
        /// </summary
        [Required]
        [Column("client_id")]
        public long ClientId { get; set; }

        /// <summary>
        /// Atribuir/Recuperar a cidade.
        /// </summary>
        [Required]
        [StringLength( 255 )]
        [Column( "city" )]
        public string City{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar dados complementares.
        /// </summary>
        [StringLength( 255 )]
        [Column( "complement" )]
        public string Complement{ get; set; }

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
        /// Atribuir/Recuperar o número.
        /// </summary>
        [Column( "number" )]
        public int? Number{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o CEP.
        /// </summary>
        [Required]
        [StringLength( 20 )]
        [Column( "postal_code" )]
        public string PostalCode{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o Estado.
        /// </summary>
        [Required]
        [StringLength( 2 )]
        [Column( "state" )]
        public string State{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar o nome da rua.
        /// </summary>
        [Required]
        [StringLength( 255 )]
        [Column( "street" )]
        public string Street{ get; set; }
        #endregion

    }
}