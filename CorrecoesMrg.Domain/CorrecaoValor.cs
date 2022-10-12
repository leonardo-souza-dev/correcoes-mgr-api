using SQLiteAbstractCrud.Proxy.Attributes;

namespace CorrecoesMgr.Domain
{
    public class CorrecaoValor : Correcao
    {
        public int Valor { get; set; }

        public CorrecaoValor(
            int id, 
            string curso, 
            DateTime data, 
            string nomeAluno, 
            int numModulo, 
            string observacao, 
            string resposta, 
            string tipo,
            int valor) : base(id, curso, data, nomeAluno, numModulo, observacao, resposta, tipo)
        {
            Valor = valor;
        }
    }
}