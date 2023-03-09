using CorrecoesMgr.Domain;

namespace CorrecoesMgr.Infra;

public interface ICorrecaoDao
{
    List<Correcao> ObterTodas();
    Correcao Obter(int id);
    Correcao Salvar(Correcao correcao);
    void Atualizar(Correcao correcao);
    bool Deletar(int id);
}