using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Touchless.Access.Data.Migrations
{
    public partial class createtablevehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    plate = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    brand = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    model = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicle_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "authentication_key",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5842), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5650), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5654), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5655), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5792), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "user_name" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5807), new TimeSpan(0, 0, 0, 0, 0)), "admin" });

            migrationBuilder.UpdateData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 21, 21, 12, 56, 337, DateTimeKind.Unspecified).AddTicks(5824), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_client_id",
                table: "vehicle",
                column: "client_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.UpdateData(
                table: "authentication_key",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(492), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(159), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(164), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(167), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(427), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "user_name" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 0, 0, 0, 0)), "adin" });

            migrationBuilder.UpdateData(
                table: "user_role",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2022, 5, 19, 3, 28, 54, 785, DateTimeKind.Unspecified).AddTicks(469), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
