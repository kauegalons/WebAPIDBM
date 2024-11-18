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

// Configura��o do banco de dados (SqlServer ou InMemoryDatabase)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Registrar os servi�os na inje��o de depend�ncia
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();

// Dentro do m�todo builder.Services:
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();



// Adicionando o FluentMigrator
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner =>
        runner.AddSqlServer()  // Usando o SqlServer como banco de dados
              .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
              .ScanIn(typeof(AddProdutosTable).Assembly)  // Escanear apenas o assembly da migra��o
    )
    .AddLogging(lb => lb.AddConsole());  // Logar a execu��o das migra��es no console

// Configura��o de controllers
builder.Services.AddControllers();

var app = builder.Build();

// Executar as migra��es ao iniciar o aplicativo
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();  // Executa todas as migra��es pendentes (Up)
}

// Configura��o do middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
