namespace Project_DisKWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableProduto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Categoria = c.String(nullable: false, maxLength: 50),
                        Ano_Lancamento = c.Int(nullable: false),
                        Autor = c.String(nullable: false, maxLength: 50),
                        Descricao = c.String(),
                        QTDE_Estoque = c.Int(nullable: false),
                        Preco_Venda = c.Double(nullable: false),
                        QTDE_Estoque_aluguel = c.Int(nullable: false),
                        Preco_Aluguel = c.Double(nullable: false),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Produto");
        }
    }
}
