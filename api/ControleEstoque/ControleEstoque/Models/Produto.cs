using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Models
{
    [Table("produto")]
    public class Produto
    {
        public int Id { get; set; }
        public string nome { get; set;}
        public int quantidade { get; set;}
    }
}
