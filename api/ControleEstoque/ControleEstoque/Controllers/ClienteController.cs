using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private EFDBContext _dbContext;

        public ClienteController(EFDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet()]
        public IEnumerable<Cliente> Get()
        {
            var clientes = _dbContext.cliente.ToList();
            return clientes;
        }

        [HttpPost()]
        public Cliente Post([FromBody] Cliente cliente)
        {
            var clientes = _dbContext.cliente.Add(cliente);
            _dbContext.SaveChanges();
            return clientes.Entity;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _dbContext.cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _dbContext.cliente.Remove(cliente);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut()]
        public IActionResult Update([FromBody] Cliente cliente)
        {
            var clienteExistente = _dbContext.cliente.FirstOrDefault(c => c.Id == cliente.Id);

            if (clienteExistente == null)
            {
                return NotFound();
            }

            clienteExistente.nome = cliente.nome;

            _dbContext.SaveChanges();

            return Ok(clienteExistente);
        }
    } 
    }