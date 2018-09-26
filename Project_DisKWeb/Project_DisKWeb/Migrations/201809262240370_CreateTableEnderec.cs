namespace Project_DisKWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableEnderec : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoId = c.Int(nullable: false, identity: true),
                        CEP = c.String(),
                        Logradouro = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.EnderecoId)
                .ForeignKey("dbo.Usuario", t => t.Usuario_UsuarioId)
                .Index(t => t.Usuario_UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Endereco", "Usuario_UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Endereco", new[] { "Usuario_UsuarioId" });
            DropTable("dbo.Endereco");
        }
    }
}
