using System.ComponentModel.DataAnnotations.Schema;

namespace CorrecoesMgr.Domain;

public class ValorModulo
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Curso { get; set; }
    public string Tipo { get; set; }
    public int NumModulo { get; set; }
    public int Valor { get; set; }
}