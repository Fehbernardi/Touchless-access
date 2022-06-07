using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touchless.Access.Data.Migrations
{
    public partial class InitialVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authentication_key",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authentication_key", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    federal_identification = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    locked = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    city = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    complement = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: true),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    street = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "last_access",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    entry_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_last_access", x => x.id);
                    table.ForeignKey(
                        name: "FK_last_access_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telephone",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    country_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    region_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telephone", x => x.id);
                    table.ForeignKey(
                        name: "FK_telephone_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "authentication_key",
                columns: new[] { "id", "active", "key", "created_at", "label" },
                values: new object[] { 1L, true, "9696AA9D-B8C2-4435-8A70-444D21601D3D", new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(492), new TimeSpan(0, 0, 0, 0, 0)), "access" });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "created_at", "name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(159), new TimeSpan(0, 0, 0, 0, 0)), "admin" },
                    { 2L, new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(164), new TimeSpan(0, 0, 0, 0, 0)), "client" },
                    { 3L, new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(167), new TimeSpan(0, 0, 0, 0, 0)), "user" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "active", "created_at", "name", "password_hash", "user_name" },
                values: new object[,]
                {
                    { 1L, true, new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(427), new TimeSpan(0, 0, 0, 0, 0)), "Felipe Bernardi", "dh2XNAvDoFUMVRHi1+iL3J02YGKK3cUOzCznOtB75XA=:u7BIRKn+TeJHaNAYs+0+5rhy6cdese345d1CS4Iy3F8=", "felipe" },
                    { 2L, true, new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 0, 0, 0, 0)), "Admin", "isbc7Rgt8ewPgznrnvHtfS3EG6RHlE69h1S1uT9Laug=:8zOzrK1ujS8l0koLk9mbhs/K26dGogm0GgpSGExQsrk=", "adin" }
                });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: new[] { "id", "created_at", "role_id", "user_id" },
                values: new object[] { 1L, new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(469), new TimeSpan(0, 0, 0, 0, 0)), 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_address_client_id",
                table: "address",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_last_access_client_id",
                table: "last_access",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_telephone_client_id",
                table: "telephone",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "authentication_key");

            migrationBuilder.DropTable(
                name: "last_access");

            migrationBuilder.DropTable(
                name: "telephone");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
