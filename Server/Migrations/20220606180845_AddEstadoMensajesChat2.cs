using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Server.Migrations
{
    public partial class AddEstadoMensajesChat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuentaUsuarios",
                table: "GruposChat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MensajesSinLeer",
                table: "GruposChat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuentaUsuarios",
                table: "GruposChat");

            migrationBuilder.DropColumn(
                name: "MensajesSinLeer",
                table: "GruposChat");
        }
    }
}
