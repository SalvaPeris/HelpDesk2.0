using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Server.Migrations
{
    public partial class AddGuidAGrupoChat3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_GruposChat_GrupoChatId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_GrupoChatId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "GrupoChatId",
                table: "Usuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GrupoChatId",
                table: "Usuarios",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoChatId",
                table: "Usuarios",
                column: "GrupoChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_GruposChat_GrupoChatId",
                table: "Usuarios",
                column: "GrupoChatId",
                principalTable: "GruposChat",
                principalColumn: "GrupoChatId");
        }
    }
}
