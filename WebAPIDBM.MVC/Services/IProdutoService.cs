using WebAPIDBM.Domain.Entities;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> GetProdutosAsync();
    Task<Produto> GetProdutoByIdAsync(int id);
    Task<bool> CreateProdutoAsync(Produto produto);
    Task<bool> UpdateProdutoAsync(Produto produto);
    Task<bool> DeleteProdutoAsync(int id);
}
