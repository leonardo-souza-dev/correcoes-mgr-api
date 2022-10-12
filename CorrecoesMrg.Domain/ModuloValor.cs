using SQLiteAbstractCrud.Proxy.Attributes;

namespace CorrecoesMgr.Domain
{
    public class ModuloValor
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public int NumModulo { get; set; }
        public int Valor { get; set; }
    
        public ModuloValor(int id, string curso, string tipo, int numModulo, int valor)
        {
            Id = id;
            Curso = curso;
            Tipo = tipo;
            NumModulo = numModulo;
            Valor = valor;
        }
    }
}