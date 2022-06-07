// =============================================================================
// ApplicationContext.cs
// 
// Autor  : Felipe Bernardi
// Data   : 10/05/2022
// =============================================================================

using Microsoft.EntityFrameworkCore;
using System;
using Touchless.Access.Data.Models;

namespace Touchless.Access.Data
{
    public class ApplicationContext : DbContext
    {
        #region Propriedades Públicas

        /// <summary>
        /// </summary>
        public virtual DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// </summary>
        public virtual DbSet<AuthenticationKey> AuthenticationKeys { get; set; }

        /// <summary>
        /// </summary>
        public virtual DbSet<Client> Clients { get; set; }

        /// <summary>
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// </summary>
        public virtual DbSet<Telephone> Telephones{ get; set; }

        /// <summary>
        /// </summary>
        public virtual DbSet<UserRole> UserRoles{ get; set; }

        /// <summary>
        /// </summary>
        public virtual DbSet<User> Users{ get; set; }

        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ApplicationContext()
        {
        }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationContext( DbContextOptions<ApplicationContext> options )
            : base( options )
        {
        }
        #endregion

        #region Métodos/Operadores Protegidos
        /// <summary>
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if( !optionsBuilder.IsConfigured ) optionsBuilder.UseNpgsql("Host=localhost;Database=control_access;Username=access;Password=aP3fVui2G7EiVNHevS5L8ZysZoabyP1fJ");
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
           

            #region Inicialização dados
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1 , CreatedAt = DateTimeOffset.UtcNow , Name = "admin" } ,
                new Role { Id = 2 , CreatedAt = DateTimeOffset.UtcNow , Name = "client" } ,
                new Role { Id = 3 , CreatedAt = DateTimeOffset.UtcNow , Name = "user" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1 ,
                    Active = true ,
                    CreatedAt = DateTimeOffset.UtcNow ,
                    Name = "Felipe Bernardi" ,
                    PasswordHash = "dh2XNAvDoFUMVRHi1+iL3J02YGKK3cUOzCznOtB75XA=:u7BIRKn+TeJHaNAYs+0+5rhy6cdese345d1CS4Iy3F8=",
                    Username = "felipe"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Active = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    Name = "Admin",
                    PasswordHash = "isbc7Rgt8ewPgznrnvHtfS3EG6RHlE69h1S1uT9Laug=:8zOzrK1ujS8l0koLk9mbhs/K26dGogm0GgpSGExQsrk=",
                    Username = "admin"
                }
            );

            modelBuilder.Entity<UserRole>().HasData( new UserRole
            {
                CreatedAt = DateTimeOffset.UtcNow , Id = 1 , RoleId = 1 , UserId = 1
            } );

            modelBuilder.Entity<AuthenticationKey>().HasData( new AuthenticationKey
            {
                Active = true , Code = "9696AA9D-B8C2-4435-8A70-444D21601D3D" , CreatedAt = DateTimeOffset.UtcNow , Id = 1 , Label = "access"
            } );
            #endregion
        }
        #endregion
    }
}