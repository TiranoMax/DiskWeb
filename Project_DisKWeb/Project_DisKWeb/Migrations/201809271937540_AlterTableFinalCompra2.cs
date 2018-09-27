namespace Project_DisKWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableFinalCompra2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FinalCompra", "Compra_CompraId", "dbo.Compra");
            DropIndex("dbo.FinalCompra", new[] { "Compra_CompraId" });
            AddColumn("dbo.Compra", "FinalCompra_FinalCompraId", c => c.Int());
            CreateIndex("dbo.Compra", "FinalCompra_FinalCompraId");
            AddForeignKey("dbo.Compra", "FinalCompra_FinalCompraId", "dbo.FinalCompra", "FinalCompraId");
            DropColumn("dbo.FinalCompra", "Compra_CompraId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FinalCompra", "Compra_CompraId", c => c.Int());
            DropForeignKey("dbo.Compra", "FinalCompra_FinalCompraId", "dbo.FinalCompra");
            DropIndex("dbo.Compra", new[] { "FinalCompra_FinalCompraId" });
            DropColumn("dbo.Compra", "FinalCompra_FinalCompraId");
            CreateIndex("dbo.FinalCompra", "Compra_CompraId");
            AddForeignKey("dbo.FinalCompra", "Compra_CompraId", "dbo.Compra", "CompraId");
        }
    }
}
