using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorrecoesMgr.Infra.Migrations
{
    public partial class Relatorio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string script =
                @"
                DROP VIEW ""main"".""Relatorio"";
                CREATE VIEW Relatorio
                AS
                --SELECT 1
                SELECT Final.Mes, SUM(Valor) Valor FROM (
                    SELECT * FROM (
		                SELECT 5 as Valor, SUBSTR(Correcoes.Data, 1, 7) Mes
		                  FROM Correcoes
		                 WHERE Correcoes.Tipo = 'Pergunta na tarefa do módulo'
		 
		                 UNION ALL
		 
		                SELECT 5 as Valor, SUBSTR(Correcoes.Data, 1, 7) Mes
		                 FROM Correcoes
		                WHERE Correcoes.Tipo = 'Fórum (Messages)'
		 
		                 UNION ALL
		 
		                SELECT 15 as Valor, SUBSTR(Correcoes.Data, 1, 7) Mes
		                  FROM Correcoes
		                 WHERE Correcoes.Tipo = 'Tarefa do módulo'
	                 ) 
                )  AS Final
                GROUP BY Final.Mes";
            var ctx = new CorrecoesMgrContext();
            ctx.Database.ExecuteSqlRaw(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var ctx = new CorrecoesMgrContext();
            ctx.Database.ExecuteSqlRaw("DROP VIEW Relatorio");
        }
    }
}
