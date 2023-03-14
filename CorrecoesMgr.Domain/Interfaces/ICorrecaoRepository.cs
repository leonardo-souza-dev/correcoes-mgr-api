using CorrecoesMgr.Domain.Entities;

namespace CorrecoesMgr.Infra;

public interface ICorrecaoRepository
{
    List<Correcao> ObterTodas();
    Correcao Obter(int id);
    Correcao Salvar(Correcao correcao);
    void Atualizar(Correcao correcao);
    bool Deletar(int id);
}