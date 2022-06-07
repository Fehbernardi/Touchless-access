// =============================================================================
// User.cs
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
    [Table( "user" )]
    public sealed class User
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar indicativo se o usuário esta ativo ou não.
        /// </summary>
        [Required]
        [Column( "active" )]
        public bool Active{ get; set; }

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
        /// Atribuir/Recuperar hash calculado da senha.
        /// </summary>
        [Required]
        [StringLength( 150 )]
        [Column( "password_hash" )]
        public string PasswordHash{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar usuário.
        /// </summary>
        [Required]
        [StringLength( 50 )]
        [Column( "user_name" )]
        public string Username{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a coleção de funções do usuário.
        /// </summary>
        public ICollection<UserRole> UserRoles{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
        #endregion
    }
}