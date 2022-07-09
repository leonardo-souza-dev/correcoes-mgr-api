﻿
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace SQLiteAbstractCrud.Tests
{
    public class Test2Fields_StrPk_Int_RepositoryTests
    {
        private string _pathFileDb;

        [SetUp]
        public void Init()
        {
            _pathFileDb = $"{Directory.GetCurrentDirectory()}/mydb.db";
            var repo = new Test2Fields_StrPk_Int_Repository(_pathFileDb);
            repo.DropTable();
        }

        [TearDown]
        public void Setup()
        {
            var repo = new Test2Fields_StrPk_Int_Repository(_pathFileDb);
            repo.DropTable();
        }

        [Test]
        public void QuandoEntidadeTiverUmCampoStringPkOutroInt_DeveObter()
        {
            // arrange
            const string valorFoo = "fooValor";
            const int valorBar = 123;
            var sut = new Test2Fields_StrPk_Int_Repository(_pathFileDb);
            sut.Insert(new Teste2Campos_StrPk_Int(valorBar, valorFoo));

            // act
            var result = sut.Get(valorFoo);

            // assert
            Assert.NotNull(result);
            Assert.AreEqual(valorFoo, result.Foo);
            Assert.AreEqual(valorBar, result.Bar);
        }

        [Test]
        public void QuandoEntidadeTiverUmCampoStringPkOutroInt_DeveInserir()
        {
            // arrange
            const string valorFoo = "fooValor";
            const int valorBar = 123;
            var entidade = new Teste2Campos_StrPk_Int(valorBar, valorFoo);
            var sut = new Test2Fields_StrPk_Int_Repository(_pathFileDb);

            // act
            sut.Insert(entidade);

            // assert
            var entidadeInserida = sut.Get(valorFoo);
            Assert.NotNull(entidadeInserida);
            Assert.AreEqual(valorFoo, entidadeInserida.Foo);
            Assert.AreEqual(valorBar, entidadeInserida.Bar);
        }

        [Test]
        public void QuandoEntidadeTiverUmCampoStringPkOutroInt_DeveAtualizar()
        {
            // arrange
            const string valorFoo = "fooValor";
            const int valorBar = 124;
            var entidade = new Teste2Campos_StrPk_Int(valorBar, valorFoo);
            var sut = new Test2Fields_StrPk_Int_Repository(_pathFileDb);
            sut.Insert(entidade);
            var novoValorBar = 445;

            // act
            _ = sut.Update(entidade, "Bar", novoValorBar);

            // assert
            var entidadeInserida = sut.Get(valorFoo);
            Assert.NotNull(entidadeInserida);
            Assert.AreEqual(valorFoo, entidadeInserida.Foo);
            Assert.AreEqual(novoValorBar, entidadeInserida.Bar);
        }

        [Test]
        public void QuandoEntidadeTiverUmCampoStringPkOutroInt_DeveInserirEmBatch()
        {
            // arrange
            var valor1 = "fooValor1";
            var valor2 = "fooValor2";
            var valor3 = "fooValor3";
            var valorInt1 = 1;
            var valorInt2 = 2;
            var valorInt3 = 3;
            var entidade1 = new Teste2Campos_StrPk_Int(valorInt1, valor1);
            var entidade2 = new Teste2Campos_StrPk_Int(valorInt2, valor2);
            var entidade3 = new Teste2Campos_StrPk_Int(valorInt3, valor3);
            
            var sut = new Test2Fields_StrPk_Int_Repository(_pathFileDb);
            
            // act
            sut.InsertBatch(new List<Teste2Campos_StrPk_Int>{ entidade1, entidade2, entidade3 });

            // assert
            var entidadeInserida1 = sut.Get(valor1);
            var entidadeInserida2 = sut.Get(valor2);
            var entidadeInserida3 = sut.Get(valor3);
            Assert.NotNull(entidadeInserida1);
            Assert.NotNull(entidadeInserida2);
            Assert.NotNull(entidadeInserida3);
            Assert.AreEqual(valor1, entidade1.Foo);
            Assert.AreEqual(valor2, entidade2.Foo);
            Assert.AreEqual(valor3, entidade3.Foo);
        }
    }

    public class Test2Fields_StrPk_Int_Repository : RepositoryBase<Teste2Campos_StrPk_Int>
    {
        public Test2Fields_StrPk_Int_Repository(string pathDbFile) : base(pathDbFile)
        {
        }
    }

    public class Teste2Campos_StrPk_Int
    {
        public int Bar { get; }
        [PrimaryKey]
        public string Foo { get; }

        public Teste2Campos_StrPk_Int(int bar, string foo)
        {
            Foo = foo;
            Bar = bar;
        }
    }
}
