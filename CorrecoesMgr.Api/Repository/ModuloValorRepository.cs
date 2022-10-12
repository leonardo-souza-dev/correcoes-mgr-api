using SQLiteAbstractCrud;
using CorrecoesMgr.Domain;

namespace CorrecoesMgr.Repository
{
    public class ModuloValorRepository : RepositoryBase<ModuloValor>
    {
        public ModuloValorRepository(string pathDbFile) : base(pathDbFile)
        {

        }
    }
}