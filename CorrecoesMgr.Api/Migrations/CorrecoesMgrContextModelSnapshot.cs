﻿// <auto-generated />
using System;
using CorrecoesMgr.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CorrecoesMgr.Api.Migrations
{
    [DbContext(typeof(CorrecoesMgrContext))]
    partial class CorrecoesMgrContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("CorrecoesMgr.Domain.Correcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Curso")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeAluno")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumModulo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Observacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resposta")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Correcoes");
                });

            modelBuilder.Entity("CorrecoesMgr.Domain.ValorModulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Curso")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumModulo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Valor")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ValoresModulo");
                });
#pragma warning restore 612, 618
        }
    }
}
