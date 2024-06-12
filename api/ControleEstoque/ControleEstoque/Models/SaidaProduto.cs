using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SaidaProduto")]
public class SaidaProduto
{
    [Key]
    public int id { get; set; }
    [ForeignKey("Produto")]
    public int idProduto { get; set; }
    [ForeignKey("Cliente")]
    public int idCliente { get; set; }
    public DateTime DataHora { get; set; }
    public int quantidade { get; set; }
    public string? NomeProduto { get; set; } 
    public string? NomeCliente { get; set; } 
}