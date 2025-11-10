using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RezervasyonApp.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "UserType",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid", "UserType" },
                values: new object[] { new DateTime(2025, 11, 10, 21, 45, 59, 566, DateTimeKind.Local).AddTicks(8849), new Guid("16d68fa0-a3d1-446d-a31a-dbe58e065840"), (byte)0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 11, 3, 21, 52, 58, 38, DateTimeKind.Local).AddTicks(4010), null });
        }
    }
}
