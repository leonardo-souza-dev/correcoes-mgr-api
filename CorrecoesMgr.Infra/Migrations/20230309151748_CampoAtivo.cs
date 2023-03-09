using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorrecoesMgr.Infra.Migrations
{
    public partial class CampoAtivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Correcoes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Correcoes");
        }
    }
}
