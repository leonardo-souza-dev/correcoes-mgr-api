using SQLiteAbstractCrud;

namespace CorrecoesMgr.Domain
{

    public class Correcao
    {
        public string Curso { get; set; }
        public DateTime Data { get; set; }
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string NomeAluno { get; set; }
        public int NumModulo { get; set; }
        public string Observacao { get; set; }
        public string Resposta { get; set; }
        public string Tipo { get; set; }
        public int Valor { get; set; }

        public Correcao(string curso, DateTime data, string nomeAluno, int numModulo, string observacao, string resposta, string tipo, int valor)
        {
            Curso = curso;
            Data = data;
            NomeAluno = nomeAluno;
            NumModulo = numModulo;
            Observacao = observacao;
            Resposta = resposta;
            Tipo = tipo;
            Valor = valor;
        }

        public Correcao(string curso, DateTime data, int id, string nomeAluno, int numModulo, string observacao,
            string resposta, string tipo, int valor)
        {
            Curso = curso;
            Data = data;
            Id = id;
            NomeAluno = nomeAluno;
            NumModulo = numModulo;
            Observacao = observacao;
            Resposta = resposta;
            Tipo = tipo;
            Valor = valor;
        }
    }
}