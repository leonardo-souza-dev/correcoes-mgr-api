using CorrecoesMgr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CorrecoesMgr.Infra
{
    public class CorrecoesMgrContext : DbContext
    {
        public DbSet<Correcao> Correcoes { get; set; }
        public DbSet<ValorModulo> ValoresModulo { get; set; }

        public string DbPath { get; }

        public CorrecoesMgrContext()
        {
#if DEBUG
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "correcoes-mgr.db");
#else
            DbPath = Path.Join(Environment.CurrentDirectory, "correcoes-mgr.db");
#endif
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var t = "Tarefa do módulo";
            var f = "Fórum (Messages)";
            var p = "Pergunta na tarefa do módulo";
            var backendJava = "Backend Java";
            var javascript = "Javascript";

            modelBuilder.Entity<ValorModulo>().HasData(
                new ValorModulo { Id = 1, Curso = backendJava, Tipo = t, NumModulo = 1, Valor = 15 },
                new ValorModulo { Id = 2, Curso = backendJava, Tipo = t, NumModulo = 2, Valor = 15 },
                new ValorModulo { Id = 3, Curso = backendJava, Tipo = t, NumModulo = 3, Valor = 15 },
                new ValorModulo { Id = 4, Curso = backendJava, Tipo = t, NumModulo = 4, Valor = 15 },
                new ValorModulo { Id = 5, Curso = backendJava, Tipo = t, NumModulo = 5, Valor = 15 },
                new ValorModulo { Id = 6, Curso = backendJava, Tipo = t, NumModulo = 6, Valor = 15 },
                new ValorModulo { Id = 7, Curso = backendJava, Tipo = t, NumModulo = 7, Valor = 15 },
                new ValorModulo { Id = 8, Curso = backendJava, Tipo = t, NumModulo = 8, Valor = 15 },
                new ValorModulo { Id = 9, Curso = backendJava, Tipo = t, NumModulo = 9, Valor = 15 },
                new ValorModulo { Id = 10, Curso = backendJava, Tipo = t, NumModulo = 10, Valor = 15 },
                new ValorModulo { Id = 11, Curso = backendJava, Tipo = t, NumModulo = 11, Valor = 15 },
                new ValorModulo { Id = 12, Curso = backendJava, Tipo = t, NumModulo = 12, Valor = 15 },
                new ValorModulo { Id = 13, Curso = backendJava, Tipo = t, NumModulo = 13, Valor = 15 },
                new ValorModulo { Id = 14, Curso = backendJava, Tipo = t, NumModulo = 14, Valor = 15 },
                new ValorModulo { Id = 15, Curso = backendJava, Tipo = t, NumModulo = 15, Valor = 15 },
                new ValorModulo { Id = 16, Curso = backendJava, Tipo = t, NumModulo = 16, Valor = 15 },
                new ValorModulo { Id = 17, Curso = backendJava, Tipo = t, NumModulo = 17, Valor = 15 },
                new ValorModulo { Id = 18, Curso = backendJava, Tipo = t, NumModulo = 18, Valor = 15 },
                new ValorModulo { Id = 19, Curso = backendJava, Tipo = t, NumModulo = 19, Valor = 15 },
                new ValorModulo { Id = 20, Curso = backendJava, Tipo = t, NumModulo = 20, Valor = 15 },
                new ValorModulo { Id = 21, Curso = backendJava, Tipo = t, NumModulo = 21, Valor = 15 },
                new ValorModulo { Id = 22, Curso = backendJava, Tipo = t, NumModulo = 22, Valor = 15 },
                new ValorModulo { Id = 23, Curso = backendJava, Tipo = t, NumModulo = 23, Valor = 15 },
                new ValorModulo { Id = 24, Curso = backendJava, Tipo = t, NumModulo = 24, Valor = 15 },
                new ValorModulo { Id = 25, Curso = backendJava, Tipo = t, NumModulo = 25, Valor = 15 },
                new ValorModulo { Id = 26, Curso = backendJava, Tipo = t, NumModulo = 26, Valor = 15 },
                new ValorModulo { Id = 27, Curso = backendJava, Tipo = t, NumModulo = 27, Valor = 15 },
                new ValorModulo { Id = 28, Curso = backendJava, Tipo = t, NumModulo = 28, Valor = 15 },
                new ValorModulo { Id = 29, Curso = backendJava, Tipo = t, NumModulo = 29, Valor = 18 },
                new ValorModulo { Id = 30, Curso = backendJava, Tipo = t, NumModulo = 30, Valor = 15 },
                new ValorModulo { Id = 31, Curso = backendJava, Tipo = t, NumModulo = 31, Valor = 15 },
                new ValorModulo { Id = 32, Curso = backendJava, Tipo = t, NumModulo = 32, Valor = 15 },
                new ValorModulo { Id = 33, Curso = backendJava, Tipo = t, NumModulo = 33, Valor = 15 },
                new ValorModulo { Id = 34, Curso = backendJava, Tipo = t, NumModulo = 34, Valor = 15 },
                new ValorModulo { Id = 35, Curso = backendJava, Tipo = t, NumModulo = 35, Valor = 15 },
                new ValorModulo { Id = 36, Curso = backendJava, Tipo = t, NumModulo = 36, Valor = 15 },
                new ValorModulo { Id = 37, Curso = backendJava, Tipo = t, NumModulo = 37, Valor = 15 },
                new ValorModulo { Id = 38, Curso = backendJava, Tipo = t, NumModulo = 38, Valor = 15 },
                new ValorModulo { Id = 39, Curso = backendJava, Tipo = t, NumModulo = 39, Valor = 15 },
                new ValorModulo { Id = 40, Curso = backendJava, Tipo = t, NumModulo = 40, Valor = 15 },
                new ValorModulo { Id = 41, Curso = backendJava, Tipo = t, NumModulo = 41, Valor = 15 },
                new ValorModulo { Id = 42, Curso = backendJava, Tipo = t, NumModulo = 42, Valor = 15 },
                new ValorModulo { Id = 43, Curso = backendJava, Tipo = t, NumModulo = 43, Valor = 15 },
                new ValorModulo { Id = 44, Curso = javascript, Tipo = t, NumModulo = 1, Valor = 15 },
                new ValorModulo { Id = 45, Curso = javascript, Tipo = t, NumModulo = 2, Valor = 15 },
                new ValorModulo { Id = 46, Curso = javascript, Tipo = t, NumModulo = 3, Valor = 15 },
                new ValorModulo { Id = 47, Curso = javascript, Tipo = t, NumModulo = 4, Valor = 15 },
                new ValorModulo { Id = 48, Curso = javascript, Tipo = t, NumModulo = 5, Valor = 15 },
                new ValorModulo { Id = 49, Curso = javascript, Tipo = t, NumModulo = 6, Valor = 15 },
                new ValorModulo { Id = 50, Curso = javascript, Tipo = t, NumModulo = 7, Valor = 18 },
                new ValorModulo { Id = 51, Curso = javascript, Tipo = t, NumModulo = 8, Valor = 15 },
                new ValorModulo { Id = 52, Curso = javascript, Tipo = t, NumModulo = 9, Valor = 15 },
                new ValorModulo { Id = 53, Curso = javascript, Tipo = t, NumModulo = 10, Valor = 18 },
                new ValorModulo { Id = 54, Curso = javascript, Tipo = t, NumModulo = 11, Valor = 15 },
                new ValorModulo { Id = 55, Curso = javascript, Tipo = t, NumModulo = 12, Valor = 15 },
                new ValorModulo { Id = 56, Curso = javascript, Tipo = t, NumModulo = 13, Valor = 15 },
                new ValorModulo { Id = 57, Curso = javascript, Tipo = t, NumModulo = 14, Valor = 15 },
                new ValorModulo { Id = 58, Curso = javascript, Tipo = t, NumModulo = 15, Valor = 15 },
                new ValorModulo { Id = 59, Curso = javascript, Tipo = t, NumModulo = 16, Valor = 15 },
                new ValorModulo { Id = 60, Curso = backendJava, Tipo = f, NumModulo = -1, Valor = 5 },
                new ValorModulo { Id = 61, Curso = javascript, Tipo = f, NumModulo = -1, Valor = 5 },
                new ValorModulo { Id = 62, Curso = backendJava, Tipo = p, NumModulo = -1, Valor = 5 },
                new ValorModulo { Id = 63, Curso = javascript, Tipo = p, NumModulo = -1, Valor = 5 }
            );

            modelBuilder.Entity<Bonus>().HasData(
                new Bonus { Id = 1, NomeCurso = backendJava,     Data = "2022-07-01", Valor = 5 },
                new Bonus { Id = 2, NomeCurso = javascript,      Data = "2022-07-01", Valor = 7 },
                new Bonus { Id = 3, NomeCurso = "Unreal Engine", Data = "2022-07-01", Valor = 7 },
                new Bonus { Id = 4, NomeCurso = backendJava,     Data = "2022-08-01", Valor = 46 },
                new Bonus { Id = 5, NomeCurso = javascript,      Data = "2022-08-01", Valor = 9 },
                new Bonus { Id = 6, NomeCurso = javascript,      Data = "2022-09-01", Valor = 24 },
                new Bonus { Id = 7, NomeCurso = backendJava,     Data = "2022-09-01", Valor = 43 },
                new Bonus { Id = 8, NomeCurso = javascript,      Data = "2022-10-01", Valor = 8 },
                new Bonus { Id = 9, NomeCurso = backendJava,     Data = "2022-10-01", Valor = 30 }
            );
        }
    }
}
