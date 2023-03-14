using CorrecoesMgr.Domain.Entities;

namespace CorrecoesMgr.Infra
{
    public class CorrecaoDao : ICorrecaoRepository
    {
        public List<Correcao> ObterTodas()
        {
            var correcoesList = new List<Correcao>();

            using var db = new CorrecoesMgrContext();
            var correcoes = db.Correcoes.Where(x => x.Ativo).OrderByDescending(x => x.Data);
            correcoesList = correcoes.ToList();

            return correcoesList;
        }

        public void Atualizar(Correcao correcao)
        {
            using var db = new CorrecoesMgrContext();
            var correcaoAtualizar = db.Correcoes.FirstOrDefault(x => x.Id == correcao.Id && x.Ativo);
            db.Entry(correcaoAtualizar).CurrentValues.SetValues(correcao);
            db.SaveChanges();
        }

        public bool Deletar(int id)
        {
            using var db = new CorrecoesMgrContext();
            var correcaoDeletar = db.Correcoes.FirstOrDefault(x => x.Id == id);

            if (correcaoDeletar == null)
            {
                return false;
            }

            correcaoDeletar.Ativo = false;
            db.Entry(correcaoDeletar).CurrentValues.SetValues(correcaoDeletar);
            db.SaveChanges();

            return true;
        }

        public Correcao Obter(int id)
        {
            using var db = new CorrecoesMgrContext();
            return db.Correcoes.FirstOrDefault(x => x.Id == id && x.Ativo);
        }

        public Correcao Salvar(Correcao correcao)
        {
            Correcao correcaoInserida;

            using var db = new CorrecoesMgrContext();
            correcaoInserida = db.Correcoes.Add(correcao).Entity;
            db.SaveChanges();

            return correcaoInserida;
        }

    }
}