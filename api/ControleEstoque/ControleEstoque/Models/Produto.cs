using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Models
{
    [Table("produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string nome { get; set;}
        public int quantidade { get; set;}
    }
}
