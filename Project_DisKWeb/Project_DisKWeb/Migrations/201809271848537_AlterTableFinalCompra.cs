namespace Project_DisKWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableFinalCompra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compra", "FinalCompra_FinalCompraId", "dbo.FinalCompra");
            DropIndex("dbo.Compra", new[] { "FinalCompra_FinalCompraId" });
            AddColumn("dbo.FinalCompra", "Compra_CompraId", c => c.Int());
            CreateIndex("dbo.FinalCompra", "Compra_CompraId");
            AddForeignKey("dbo.FinalCompra", "Compra_CompraId", "dbo.Compra", "CompraId");
            DropColumn("dbo.Compra", "FinalCompra_FinalCompraId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compra", "FinalCompra_FinalCompraId", c => c.Int());
            DropForeignKey("dbo.FinalCompra", "Compra_CompraId", "dbo.Compra");
            DropIndex("dbo.FinalCompra", new[] { "Compra_CompraId" });
            DropColumn("dbo.FinalCompra", "Compra_CompraId");
            CreateIndex("dbo.Compra", "FinalCompra_FinalCompraId");
            AddForeignKey("dbo.Compra", "FinalCompra_FinalCompraId", "dbo.FinalCompra", "FinalCompraId");
        }
    }
}
