using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorrecoesMgr.Api.Migrations
{
    public partial class Relatorio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string script =
                @"
                DROP VIEW IF EXISTS ""Relatorio"";
                CREATE VIEW Relatorio
                AS
                --SELECT 1
                SELECT Final.Mes, SUM(Valor) Valor FROM (
	                SELECT ModuloValorPergunta.Valor, SUBSTR(Correcao.Data, 1, 7) Mes, 'Pergunta' Tipo FROM Correcao
	                 INNER JOIN ModuloValor ModuloValorPergunta ON ModuloValorPergunta.Curso = Correcao.Curso AND ModuloValorPergunta.Tipo = Correcao.Tipo AND Correcao.Tipo = 'Pergunta na tarefa do módulo'
	 
	                 UNION ALL
	 
	                SELECT ModuloValorForum.Valor, SUBSTR(Correcao.Data, 1, 7) Mes, 'Fórum' Tipo FROM Correcao
	                 INNER JOIN ModuloValor ModuloValorForum    ON    ModuloValorForum.Curso = Correcao.Curso AND ModuloValorForum.Tipo = Correcao.Tipo AND Correcao.Tipo = 'Fórum (Messages)'
	 
	                 UNION ALL
	 
	                SELECT ModuloValorTarefa.Valor, SUBSTR(Correcao.Data, 1, 7) Mes, 'Tarefa' Tipo FROM Correcao
	                 INNER JOIN ModuloValor ModuloValorTarefa   ON   ModuloValorTarefa.Curso = Correcao.Curso AND ModuloValorTarefa.Tipo = Correcao.Tipo AND ModuloValorTarefa.NumModulo = Correcao.NumModulo
	 
	                 UNION ALL
	
	                SELECT  Valor, SUBSTR(Data, 1, 7) as Mes, 'Bonus' FROM Bonus  
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
