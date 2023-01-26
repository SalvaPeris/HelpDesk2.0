using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Server.Migrations
{
    public partial class AEstadoMensajesChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_GruposChat_GrupoChatId",
                table: "Chats");

            migrationBuilder.AlterColumn<Guid>(
                name: "GrupoChatId",
                table: "Chats",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "MensajeChatUsuario",
                columns: table => new
                {
                    MensajeChatId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Leido = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaLeido = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajeChatUsuario", x => new { x.MensajeChatId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_MensajeChatUsuario_Chats_MensajeChatId",
                        column: x => x.MensajeChatId,
                        principalTable: "Chats",
                        principalColumn: "MensajeChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensajeChatUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MensajeChatUsuario_UsuarioId",
                table: "MensajeChatUsuario",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_GruposChat_GrupoChatId",
                table: "Chats",
                column: "GrupoChatId",
                principalTable: "GruposChat",
                principalColumn: "GrupoChatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_GruposChat_GrupoChatId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "MensajeChatUsuario");

            migrationBuilder.AlterColumn<Guid>(
                name: "GrupoChatId",
                table: "Chats",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_GruposChat_GrupoChatId",
                table: "Chats",
                column: "GrupoChatId",
                principalTable: "GruposChat",
                principalColumn: "GrupoChatId");
        }
    }
}
