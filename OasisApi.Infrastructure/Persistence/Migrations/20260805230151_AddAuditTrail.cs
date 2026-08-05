using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OasisApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditTrail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Moradores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Moradores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Moradores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Moradores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Alojamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Alojamentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Alojamentos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Alojamentos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Alojamentos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedAt", "UpdatedByUserId" },
                values: new object[] { new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null });

            migrationBuilder.UpdateData(
                table: "Moradores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedByUserId", "UpdatedAt", "UpdatedByUserId" },
                values: new object[] { new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Moradores_CreatedByUserId",
                table: "Moradores",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Moradores_UpdatedByUserId",
                table: "Moradores",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alojamentos_CreatedByUserId",
                table: "Alojamentos",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alojamentos_UpdatedByUserId",
                table: "Alojamentos",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alojamentos_Users_CreatedByUserId",
                table: "Alojamentos",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alojamentos_Users_UpdatedByUserId",
                table: "Alojamentos",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moradores_Users_CreatedByUserId",
                table: "Moradores",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moradores_Users_UpdatedByUserId",
                table: "Moradores",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alojamentos_Users_CreatedByUserId",
                table: "Alojamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alojamentos_Users_UpdatedByUserId",
                table: "Alojamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Moradores_Users_CreatedByUserId",
                table: "Moradores");

            migrationBuilder.DropForeignKey(
                name: "FK_Moradores_Users_UpdatedByUserId",
                table: "Moradores");

            migrationBuilder.DropIndex(
                name: "IX_Moradores_CreatedByUserId",
                table: "Moradores");

            migrationBuilder.DropIndex(
                name: "IX_Moradores_UpdatedByUserId",
                table: "Moradores");

            migrationBuilder.DropIndex(
                name: "IX_Alojamentos_CreatedByUserId",
                table: "Alojamentos");

            migrationBuilder.DropIndex(
                name: "IX_Alojamentos_UpdatedByUserId",
                table: "Alojamentos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Moradores");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Moradores");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Moradores");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Moradores");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Alojamentos");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Alojamentos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Alojamentos");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Alojamentos");
        }
    }
}
