using System.ComponentModel.DataAnnotations.Schema;

[Table("SaidaProduto")]
public class SaidaProduto
{
    public int id { get; set; }
    public int idProduto { get; set; }
    public int idCliente { get; set; }
    public DateTime DataHora { get; set; }
    public int quantidade { get; set; }
    public string? NomeProduto { get; set; } 
    public string? NomeCliente { get; set; } 
}