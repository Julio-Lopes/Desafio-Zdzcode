using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private EFDBContext _dbContext;

        public ProdutoController(EFDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet()]
        public IEnumerable<Produto> Get()
        {
            var produtos = _dbContext.produto.ToList();
            return produtos;
        }

        [HttpPost()]
        public Produto Post([FromBody] Produto produto)
        {
            var produtos = _dbContext.produto.Add(produto);
            _dbContext.SaveChanges();
            return produtos.Entity;
        }

        [HttpDelete()]
        public Produto Delete([FromBody] Produto produto)
        {
            var produtos = _dbContext.produto.Remove(produto);
            _dbContext.SaveChanges();
            return produtos.Entity;
        }

        [HttpPut()]
        public IActionResult Update([FromBody] Produto produto)
        {
            var produtoExistente = _dbContext.produto.FirstOrDefault(p => p.Id == produto.Id);

            if (produtoExistente == null)
            {
                return NotFound();
            }

            produtoExistente.nome = produto.nome;
            produtoExistente.quantidade = produto.quantidade;

            _dbContext.SaveChanges();

            return Ok(produtoExistente);
        }
    }
    }