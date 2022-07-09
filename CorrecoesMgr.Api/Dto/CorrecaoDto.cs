using CorrecoesMgr.Domain;


namespace CorrecoesMgr.Dto
{
    public class CorrecaoDto
    {
        public DateTime Data { get; set; }
        public string Curso { get; set; }
        public string NomeAluno { get; set; }
        public byte NumModulo { get; set; }
        public string Tipo { get; set; }
        public string Resposta { get; set; }
        public int Valor { get; set; }
        public string Observacao { get; set; }

        public CorrecaoDto(DateTime data, string curso, string nomeAluno, byte numModulo, string tipo, string resposta,
            int valor, string observacao)
        {
            Data = data;
            Curso = curso;
            NomeAluno = nomeAluno;
            NumModulo = numModulo;
            Tipo = tipo;
            Resposta = resposta;
            Valor = valor;
            Observacao = observacao;
        }

        public Correcao ToModel()
        {
            return new Correcao(Curso, Data, NomeAluno, NumModulo, Observacao, Resposta, Tipo, Valor);
        }
    }
}