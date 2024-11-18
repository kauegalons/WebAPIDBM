using FluentValidation;
using WebAPIDBM.Domain.Entities;
using FluentValidation.Results;


public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator(WebAPIDBM.Domain.Interfaces.IProdutoRepository @object)
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome do produto não pode ser vazio.")
            .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");

        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.")
            .NotNull().WithMessage("O preço do produto não pode ser nulo.");

        RuleFor(p => p.Estoque)
            .GreaterThanOrEqualTo(0).WithMessage("O estoque não pode ser negativo.");
    }

    public async Task<FluentValidation.Results.ValidationResult> ValidarProdutoAsync(Produto produto)
    {
        return await ValidateAsync(produto);
    }

}