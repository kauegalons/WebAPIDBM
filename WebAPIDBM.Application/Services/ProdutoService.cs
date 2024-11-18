using WebAPIDBM.Domain.Entities;
using WebAPIDBM.Domain.Interfaces;

namespace WebAPIDBM.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync() => await _produtoRepository.GetAllAsync();
        public async Task<Produto> GetByIdAsync(int id) => await _produtoRepository.GetByIdAsync();
        public async Task AddAsync(Produto produto) => await _produtoRepository.AddAsync(produto);
        public async Task UpdateAsync(Produto produto) => await _produtoRepository.UpdateAsync(produto);
        public async Task DeleteAsync(int id) => await _produtoRepository.DeleteAsync(id);

    }
}
