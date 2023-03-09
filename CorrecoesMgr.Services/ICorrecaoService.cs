using CorrecoesMgr.Domain;

namespace CorrecoesMgr.Services
{
    public interface ICorrecaoService
    {
        List<Correcao> ObterTodas();
        Correcao Obter(int id);
        Correcao Salvar(Correcao correcao);
        void Atualizar(Correcao correcao);
        bool Deletar(int id);
    }
}