using SQLiteAbstractCrud;
using CorrecoesMgr.Domain;

namespace CorrecoesMgr.Repository
{

    public class CorrecaoRepository : RepositoryBase<Correcao>
    {
        public CorrecaoRepository(string pathDbFile) : base(pathDbFile)
        {

        }
    }
}