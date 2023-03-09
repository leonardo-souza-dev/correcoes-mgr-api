using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorrecoesMgr.Infra.Migrations
{
    public partial class Bonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCurso = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 1, "2022-07-01", "Backend Java", 5 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 2, "2022-07-01", "Javascript", 7 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 3, "2022-07-01", "Unreal Engine", 7 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 4, "2022-08-01", "Backend Java", 46 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 5, "2022-08-01", "Javascript", 9 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 6, "2022-09-01", "Javascript", 24 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 7, "2022-09-01", "Backend Java", 43 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 8, "2022-10-01", "Javascript", 8 });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Data", "NomeCurso", "Valor" },
                values: new object[] { 9, "2022-10-01", "Backend Java", 30 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus");
        }
    }
}
