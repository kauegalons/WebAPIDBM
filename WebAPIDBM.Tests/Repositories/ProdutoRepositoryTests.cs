using Microsoft.EntityFrameworkCore;
using WebAPIDBM.Domain.Entities;
using WebAPIDBM.Infrastructure.Data;
using WebAPIDBM.Infrastructure.Repositories;
using Xunit;

public class ProdutoRepositoryTests
{
    private readonly ProdutoRepository _repository;
    private readonly AppDbContext _context;

    public ProdutoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDB")
            .Options;

        _context = new AppDbContext(options);
        _repository = new ProdutoRepository(_context);
    }

    [Fact]
    public async Task Deve_Adicionar_Produto()
    {
        var produto = new Produto { Nome = "Novo Produto", Preco = 50.0m, Estoque = 10 };

        await _repository.AddAsync(produto);
        var produtoSalvo = await _repository.GetByIdAsync(produto.Id);

        Assert.NotNull(produtoSalvo);
        Assert.Equal("Novo Produto", produtoSalvo.Nome);
    }

    [Fact]
    public async Task Deve_Retornar_Todos_Os_Produtos()
    {
        var produtos = new[]
        {
            new Produto { Nome = "Produto 1", Preco = 10.0m, Estoque = 5 },
            new Produto { Nome = "Produto 2", Preco = 20.0m, Estoque = 10 },
        };

        _context.Produtos.AddRange(produtos);
        await _context.SaveChangesAsync();

        var resultado = await _repository.GetAllAsync();

        Assert.Equal(2, resultado.Count());
    }
}
