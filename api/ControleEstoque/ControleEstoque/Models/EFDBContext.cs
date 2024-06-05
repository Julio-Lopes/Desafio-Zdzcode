using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleEstoque.Models
{
    public class EFDBContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Produto> produto {  get; set; }

        public DbSet<Cliente> cliente { get; set; }

        public DbSet<SaidaProduto> saidaProduto { get; set; }

        public EFDBContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDataBase = _configuration["TypeDataBase"];
            var connectionString = _configuration.GetConnectionString(typeDataBase);

            if (typeDataBase == "MYSQL") {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
