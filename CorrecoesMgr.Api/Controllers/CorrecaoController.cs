using CorrecoesMgr.Api;
using CorrecoesMgr.Api.Models;

namespace CorrecoesMgr.Repository
{
    public class CorrecaoController
    {
        public CorrecaoController(WebApplication app)
        {
            app.MapGet("/correcoes", () => ObterCorrecoes());
            app.MapPut("/inserir-correcao", (Correcao correcao) => InserirCorrecao(correcao));
        }

        public IResult ObterCorrecoes()
        {
            var correcoesList = new List<Correcao>();
            using (var db = new CorrecoesMgrContext())
            {
                var correcoes = db.Correcoes.OrderBy(x => x.Id);
                correcoesList = correcoes.ToList();
            }

            return Results.Ok(correcoesList);
        }

        public IResult InserirCorrecao(Correcao correcao)
        {
            Correcao correcaoInserida;
            using (var db = new CorrecoesMgrContext())
            {
                correcaoInserida = db.Correcoes.Add(correcao).Entity;
                db.SaveChanges();
            }

            return Results.Ok(correcaoInserida);
        }
    }
}