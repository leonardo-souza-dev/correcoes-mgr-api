﻿using CorrecoesMgr.Domain;
using Microsoft.EntityFrameworkCore;

namespace CorrecoesMgr.Api
{
    public class CorrecoesMgrContext : DbContext
    {
        public DbSet<Correcao> Correcoes { get; set; }
        public DbSet<ValorModulo> ValoresModulo { get; set; }

        public string DbPath { get; }

        public CorrecoesMgrContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "correcoes-mgr.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}