using System.ComponentModel.DataAnnotations.Schema;

namespace CorrecoesMgr.Domain;

[Table("Correcoes")]
public class Correcao
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Curso { get; set; }
    public DateTime Data { get; set; }
    public string NomeAluno { get; set; }
    public int NumModulo { get; set; }
    public string? Observacao { get; set; }
    public string Resposta { get; set; }
    public string Tipo { get; set; }
    public bool Ativo { get; set; }

    public Correcao(
        int id, 
        string curso, 
        DateTime data, 
        string nomeAluno, 
        int numModulo, 
        string observacao,
        string resposta, 
        string tipo,
        bool ativo)
    {
        Curso = curso;
        Data = data;
        Id = id;
        NomeAluno = nomeAluno;
        NumModulo = numModulo;
        Observacao = observacao;
        Resposta = resposta;
        Tipo = tipo;
        Ativo = ativo;
    }
}