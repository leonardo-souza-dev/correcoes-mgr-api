using System.ComponentModel.DataAnnotations.Schema;

namespace CorrecoesMgr.Domain.Entities;

[Table("Relatorio")]
public class Relatorio
{
    public string Mes { get; set; }
    public int Valor { get; set; }
}