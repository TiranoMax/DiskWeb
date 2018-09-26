namespace Project_DisKWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableFinalCompra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinalCompra",
                c => new
                    {
                        FinalCompraId = c.Int(nullable: false, identity: true),
                        Endereco_EnderecoId = c.Int(),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.FinalCompraId)
                .ForeignKey("dbo.Endereco", t => t.Endereco_EnderecoId)
                .ForeignKey("dbo.Usuario", t => t.Usuario_UsuarioId)
                .Index(t => t.Endereco_EnderecoId)
                .Index(t => t.Usuario_UsuarioId);
            
            AddColumn("dbo.Compra", "FinalCompra_FinalCompraId", c => c.Int());
            CreateIndex("dbo.Compra", "FinalCompra_FinalCompraId");
            AddForeignKey("dbo.Compra", "FinalCompra_FinalCompraId", "dbo.FinalCompra", "FinalCompraId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FinalCompra", "Usuario_UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.FinalCompra", "Endereco_EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Compra", "FinalCompra_FinalCompraId", "dbo.FinalCompra");
            DropIndex("dbo.FinalCompra", new[] { "Usuario_UsuarioId" });
            DropIndex("dbo.FinalCompra", new[] { "Endereco_EnderecoId" });
            DropIndex("dbo.Compra", new[] { "FinalCompra_FinalCompraId" });
            DropColumn("dbo.Compra", "FinalCompra_FinalCompraId");
            DropTable("dbo.FinalCompra");
        }
    }
}
