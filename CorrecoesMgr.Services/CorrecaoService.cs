using CorrecoesMgr.Domain;
using CorrecoesMgr.Infra;

namespace CorrecoesMgr.Services;

public class CorrecaoService : ICorrecaoService
{
    private readonly ICorrecaoDao _correcaoDao;

    public CorrecaoService(ICorrecaoDao correcaoDao) => _correcaoDao = correcaoDao;

    public List<Correcao> ObterTodas() => _correcaoDao.ObterTodas();
    
    public void Atualizar(Correcao correcao) => _correcaoDao.Atualizar(correcao);

    public bool Deletar(int id) => _correcaoDao.Deletar(id);

    public Correcao Obter(int id) => _correcaoDao.Obter(id);

    public Correcao Salvar(Correcao correcao) => _correcaoDao.Salvar(correcao);
}