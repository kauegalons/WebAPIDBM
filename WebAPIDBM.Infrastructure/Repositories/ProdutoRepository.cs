using Microsoft.EntityFrameworkCore;
using WebAPIDBM.Domain.Entities;
using WebAPIDBM.Domain.Interfaces;
using WebAPIDBM.Infrastructure.Data;

namespace WebAPIDBM.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteNomeAsync(string nome)
        {
            return await _context.Produtos
                .AnyAsync(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public Task<Produto> GetByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
