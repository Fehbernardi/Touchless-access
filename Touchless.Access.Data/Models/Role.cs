// =============================================================================
// Role.cs
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
    [Table( "role" )]
    public sealed class Role
    {
        #region Propriedades Públicas
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
        /// Atribuir/Recuperar nome.
        /// </summary>
        [Required]
        [StringLength( 100 )]
        [Column( "name" )]
        public string Name{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de funções do usuário.
        /// </summary>
        public ICollection<UserRole> UserRoles{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        #endregion
    }
}