using Moq;
using WebAPIDBM.Application.Services;
using WebAPIDBM.Domain.Entities;
using WebAPIDBM.Domain.Interfaces;
using Xunit;

public class ProdutoServiceTests
{
    private readonly ProdutoService _service;
    private readonly Mock<IProdutoRepository> _repositorioMock;

    public ProdutoServiceTests()
    {
        _repositorioMock = new Mock<IProdutoRepository>();
        _service = new ProdutoService(_repositorioMock.Object);
    }

    [Fact]
    public async Task Deve_Adicionar_Produto_Com_Sucesso()
    {
        var produto = new Produto { Nome = "Produto Teste", Preco = 100.0m, Estoque = 10 };

        await _service.AddAsync(produto);

        _repositorioMock.Verify(r => r.AddAsync(produto), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Exception_Se_Preco_Eh_Negativo()
    {
        var produto = new Produto { Nome = "Produto Teste", Preco = -10.0m, Estoque = 5 };

        await Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(produto));
    }
}
