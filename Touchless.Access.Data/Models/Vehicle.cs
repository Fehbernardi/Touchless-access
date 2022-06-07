// =============================================================================
// Vehicle.cs
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
    [Table( "vehicle" )]
    public sealed class Vehicle
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
        /// Atribuir/Recuperar a placa do veiculo.
        /// </summary>
        [Required]
        [StringLength( 8 )]
        [Column( "plate" )]
        public string Plate { get; set; }

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
        /// Atribuir/Recuperar a marca do veiculo.
        /// </summary>
        [Required]
        [StringLength( 20 )]
        [Column( "brand" )]
        public string Brand { get; set; }

        /// <summary>
        /// Atribuir/Recuperar modelo do veiculo.
        /// </summary>
        [Required]
        [StringLength( 25 )]
        [Column( "model" )]
        public string Modelo{ get; set; }
        #endregion

    }
}