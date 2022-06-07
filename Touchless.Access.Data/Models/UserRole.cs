// =============================================================================
// UserRole.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/05/2022
// =============================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Touchless.Access.Data.Models
{
    [Table( "user_role" )]
    public class UserRole
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
        /// Atribuir/Recuperar objeto contendo as informações da função.
        /// </summary>
        public Role Role{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador da função.
        /// </summary>
        [Column( "role_id" )]
        [Required]
        public long RoleId{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar objeto contendo as informações do usuário.
        /// </summary>
        public User User{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar identificador do usuário.
        /// </summary>
        [Required]
        [Column( "user_id" )]
        public long UserId{ get; set; }
        #endregion
    }
}