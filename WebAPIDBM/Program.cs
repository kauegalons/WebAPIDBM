using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using WebAPIDBM.Application.Services;
using WebAPIDBM.Infrastructure.Data;
using WebAPIDBM.Infrastructure.Repositories;
using WebAPIDBM.Domain.Interfaces;
using WebAPIDBM.Infrastructure.Migrations;
using FluentValidation.AspNetCore;
using WebAPIDBM.Application.Validators;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados (SqlServer ou InMemoryDatabase)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Registrar os serviços na injeção de dependência
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();

// Dentro do método builder.Services:
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();



// Adicionando o FluentMigrator
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner =>
        runner.AddSqlServer()  // Usando o SqlServer como banco de dados
              .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
              .ScanIn(typeof(AddProdutosTable).Assembly)  // Escanear apenas o assembly da migração
    )
    .AddLogging(lb => lb.AddConsole());  // Logar a execução das migrações no console

// Configuração de controllers
builder.Services.AddControllers();

var app = builder.Build();

// Executar as migrações ao iniciar o aplicativo
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();  // Executa todas as migrações pendentes (Up)
}

// Configuração do middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
