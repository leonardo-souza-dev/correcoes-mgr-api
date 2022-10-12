using SQLiteAbstractCrud;
using CorrecoesMgr.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CorrecoesMgr.Repository
{
    public class CorrecaoController
    {
        private readonly WebApplication _app;
        private readonly CorrecaoRepository _correcaoRepository;
        private readonly ModuloValorRepository _moduloValorRepository;

        public CorrecaoController(
            WebApplication app, 
            CorrecaoRepository correcaoRepository,
            ModuloValorRepository moduloValorRepository)
        {
            _app = app;
            _correcaoRepository = correcaoRepository;
            _moduloValorRepository = moduloValorRepository;

            app.MapGet("/correcoes", () => ObterCorrecoes());
            app.MapPut("/inserir-correcao", (Correcao correcao) => InserirCorrecao(correcao));
        }

        public IResult ObterCorrecoes()
        {
            var correcoes = _correcaoRepository.GetAll();
            var valoresModulos = _moduloValorRepository.GetAll();

            var correcoesValores = new List<dynamic>();

            foreach (var valorModulo in valoresModulos)
            {
                foreach (var correcao in correcoes)
                {
                    if (correcao.Curso == valorModulo.Curso &&
                        correcao.Tipo == valorModulo.Tipo &&
                        correcao.NumModulo == valorModulo.NumModulo)
                    {
                        correcoesValores.Add(new
                        {
                            correcao.Id,
                            correcao.Curso,
                            correcao.Data,
                            correcao.NomeAluno,
                            correcao.NumModulo,
                            correcao.Observacao,
                            correcao.Resposta,
                            correcao.Tipo,
                            valorModulo.Valor
                        });
                    }
                }
            }

            return Results.Ok(correcoesValores.OrderBy(x => x.Id));
        }

        public IResult InserirCorrecao(Correcao correcao)
        {
            var ultimaCorrecao = _correcaoRepository.GetAll()?.OrderByDescending(x => x.Id)?.First();

            if (ultimaCorrecao == null)
            {
                throw new AggregateException("erro ao obter ultima correcao");
            }

            correcao.Id = ultimaCorrecao.Id + 1;

            var correcaoInserida = _correcaoRepository.Insert(correcao);

            var moduloValor = _moduloValorRepository.GetAll()
                .First(x => x.Curso == correcaoInserida.Curso &&
                x.Tipo == correcaoInserida.Tipo &&
                x.NumModulo == correcaoInserida.NumModulo);

            var correcaoValor = new CorrecaoValor(
                correcaoInserida.Id,
                correcaoInserida.Curso,
                correcaoInserida.Data,
                correcaoInserida.NomeAluno,
                correcaoInserida.NumModulo,
                correcaoInserida.Observacao,
                correcaoInserida.Resposta,
                correcaoInserida.Tipo,
                moduloValor.Valor);

            return Results.Ok(correcaoValor);
        }
    }
}