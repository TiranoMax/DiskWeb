namespace Project_DisKWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCompra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compra",
                c => new
                    {
                        CompraId = c.Int(nullable: false, identity: true),
                        Qtde = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        DataDevolucao = c.DateTime(nullable: false),
                        Multa = c.Double(nullable: false),
                        Valor = c.Double(nullable: false),
                        CarTId = c.String(),
                        Produto_ProdutoId = c.Int(),
                    })
                .PrimaryKey(t => t.CompraId)
                .ForeignKey("dbo.Produto", t => t.Produto_ProdutoId)
                .Index(t => t.Produto_ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compra", "Produto_ProdutoId", "dbo.Produto");
            DropIndex("dbo.Compra", new[] { "Produto_ProdutoId" });
            DropTable("dbo.Compra");
        }
    }
}
