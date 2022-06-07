// =============================================================================
// Telephone.cs
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
    [Table( "telephone" )]
    public sealed class Telephone
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar Objeto do cliente.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador unico do cliente.
        /// </summary>
        [Required]
        [Column("client_id")]
        public long ClientId { get; set; }

        /// <summary>
        /// Atribuir/Recuperar código do país.
        /// </summary>
        [Required]
        [StringLength( 5 )]
        [Column( "country_code" )]
        public string CountryCode{ get; set; }

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
        /// Atribuir/Recuperar número do telefone.
        /// </summary>
        [Required]
        [StringLength( 20 )]
        [Column( "number" )]
        public string Number{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar código da região.
        /// </summary>
        [Required]
        [StringLength( 5 )]
        [Column( "region_code" )]
        public string RegionCode{ get; set; }
        #endregion

    }
}