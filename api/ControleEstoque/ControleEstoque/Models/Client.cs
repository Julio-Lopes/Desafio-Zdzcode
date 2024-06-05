using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Models
{
    [Table("cliente")]
    public class Cliente
    {
        public int Id { get; set; }
        public string nome { get; set;}
    }
}
