using FluentValidation.TestHelper;
using Moq;
using WebAPIDBM.Application.Validators;
using WebAPIDBM.Domain.Entities;
using WebAPIDBM.Domain.Interfaces;
using Xunit;
using FluentValidation;
using FluentValidation.Results;


public class ProdutoValidatorTests
{
    private readonly ProdutoValidator _validator;
    private readonly Mock<IProdutoRepository> _repositorioMock;

    public ProdutoValidatorTests()
    {
        _repositorioMock = new Mock<IProdutoRepository>();
        _repositorioMock.Setup(r => r.ExisteNomeAsync(It.IsAny<string>()))
            .ReturnsAsync(false); // Nome não existe por padrão
        _validator = new ProdutoValidator(_repositorioMock.Object);
    }

    [Fact]
    public async Task Deve_Passar_Quando_Produto_For_Valido()
    {
        var produto = new Produto
        {
            Nome = "Produto Válido",
            Preco = 10.0m,
            Estoque = 5
        };

        var resultado = await _validator.ValidarProdutoAsync(produto);
        Assert.True(resultado.IsValid, "O produto deve ser considerado válido.");
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Nome_Eh_Vazio()
    {
        var produto = new Produto
        {
            Nome = "",
            Preco = 10.0m,
            Estoque = 5
        };

        var resultado = await _validator.ValidarProdutoAsync(produto);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage.Contains("não pode ser vazio"));
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Preco_Eh_Negativo()
    {
        var produto = new Produto
        {
            Nome = "Produto",
            Preco = -1.0m,
            Estoque = 5
        };

        var resultado = await _validator.ValidarProdutoAsync(produto);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "Preco" && e.ErrorMessage.Contains("maior que zero"));
    }
}
