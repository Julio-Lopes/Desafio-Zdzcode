using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaidaProdutoController : ControllerBase
    {
        private readonly EFDBContext _context;

        public SaidaProdutoController(EFDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IEnumerable<SaidaProduto>> Get()
        {
            var query = @"
                SELECT sp.id, sp.idProduto, sp.idCliente, sp.DataHora, sp.quantidade, p.nome AS NomeProduto, c.nome AS NomeCliente
                FROM SaidaProduto sp
                INNER JOIN Produto p ON sp.idProduto = p.id
                INNER JOIN Cliente c ON sp.idCliente = c.id;";

            return await _context.saidaProduto
                .FromSqlRaw(query)
                .ToListAsync();
        }

        [HttpPost()]
        public SaidaProduto Post([FromBody] SaidaProduto saidaProduto)
        {
            var saidaProdutos = _context.saidaProduto.Add(saidaProduto);
            _context.SaveChanges();
            return saidaProdutos.Entity;
        }

        [HttpDelete("{id}")]
            public async Task<ActionResult<SaidaProduto>> Delete(int id)
            {
                var saidaProduto = await _context.saidaProduto.FindAsync(id);
                if (saidaProduto == null)
                {
                    return NotFound();
                }

                _context.saidaProduto.Remove(saidaProduto);
                await _context.SaveChangesAsync();

                return saidaProduto;
            }
        }
}
