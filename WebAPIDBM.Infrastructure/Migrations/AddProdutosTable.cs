using FluentMigrator;

namespace WebAPIDBM.Infrastructure.Migrations
{
    [Migration(2024111701)]  // Número de versão único para a migração
    public class AddProdutosTable : Migration
    {
        public override void Up()
        {
            Create.Table("Produtos")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("Preco").AsDecimal().NotNullable()
                .WithColumn("Estoque").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Produtos");
        }
    }
}
