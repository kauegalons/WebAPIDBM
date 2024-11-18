using Microsoft.EntityFrameworkCore;
using WebAPIDBM.Domain.Entities;

namespace WebAPIDBM.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definir o DbSet para a entidade Produto
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Produto>().Property(p => p.Preco).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Estoque).IsRequired();
        }
    }
}
