// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Touchless.Access.Data;

#nullable disable

namespace Touchless.Access.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220519032855_InitialVersion")]
    partial class InitialVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Touchless.Access.Data.Models.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("city");

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint")
                        .HasColumnName("client_id");

                    b.Property<string>("Complement")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("complement");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int?>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("postal_code");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("street");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("address");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.AuthenticationKey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("key");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("label");

                    b.HasKey("Id");

                    b.ToTable("authentication_key");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            Code = "9696AA9D-B8C2-4435-8A70-444D21601D3D",
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(492), new TimeSpan(0, 0, 0, 0, 0)),
                            Label = "access"
                        });
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("FederalIdentification")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("federal_identification");

                    b.Property<bool>("Locked")
                        .HasColumnType("boolean")
                        .HasColumnName("locked");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("client");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.LastAccess", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint")
                        .HasColumnName("client_id");

                    b.Property<DateTimeOffset>("EntryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("entry_time");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("last_access");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("role");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(159), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(164), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "client"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(167), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "user"
                        });
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Telephone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint")
                        .HasColumnName("client_id");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("country_code");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("number");

                    b.Property<string>("RegionCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("region_code");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("telephone");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.ToTable("user");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(427), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Felipe Bernardi",
                            PasswordHash = "dh2XNAvDoFUMVRHi1+iL3J02YGKK3cUOzCznOtB75XA=:u7BIRKn+TeJHaNAYs+0+5rhy6cdese345d1CS4Iy3F8=",
                            Username = "felipe"
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Admin",
                            PasswordHash = "isbc7Rgt8ewPgznrnvHtfS3EG6RHlE69h1S1uT9Laug=:8zOzrK1ujS8l0koLk9mbhs/K26dGogm0GgpSGExQsrk=",
                            Username = "adin"
                        });
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("user_role");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(469), new TimeSpan(0, 0, 0, 0, 0)),
                            RoleId = 1L,
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Address", b =>
                {
                    b.HasOne("Touchless.Access.Data.Models.Client", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.LastAccess", b =>
                {
                    b.HasOne("Touchless.Access.Data.Models.Client", "Client")
                        .WithMany("LastAccess")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Telephone", b =>
                {
                    b.HasOne("Touchless.Access.Data.Models.Client", "Client")
                        .WithMany("Telephones")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.UserRole", b =>
                {
                    b.HasOne("Touchless.Access.Data.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Touchless.Access.Data.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Client", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("LastAccess");

                    b.Navigation("Telephones");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Touchless.Access.Data.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
