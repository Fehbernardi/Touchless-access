// =============================================================================
// Client.cs
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
    [Table("client")]
    public sealed class Client
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se o cliente esta ativo ou não.
        /// </summary>
        [Required]
        [Column("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de endereços.
        /// </summary>
        public ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de telefones.
        /// </summary>
        public ICollection<Telephone> Telephones { get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de veiculos.
        /// </summary>
        public ICollection<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Atribuir/Recuperar a data de criação.
        /// </summary>
        [Required]
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Atribuir/Recuperar CPF ou CNPJ.
        /// </summary>
        [Required]
        [StringLength(14)]
        [Column("federal_identification")]
        public string FederalIdentification { get; set; }

        /// <summary>
        /// Atribuir/Recuperar o identificador do registro.
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Atribuir/Recuperar Bloqueio
        /// </summary>
        [Required]
        [Column("locked")]
        public bool Locked { get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de endereços.
        /// </summary>
        public ICollection<LastAccess> LastAccess { get; set; }

        /// <summary>
        /// Atribuir/Recuperar nome.
        /// </summary>
        [Required]
        [StringLength(255)]
        [Column("name")]
        public string Name { get; set; }

        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public Client()
        {
            Addresses = new HashSet<Address>();
            Telephones = new HashSet<Telephone>();
            LastAccess = new HashSet<LastAccess>();
            Vehicles = new HashSet<Vehicle>();
        }
        #endregion
    }
}