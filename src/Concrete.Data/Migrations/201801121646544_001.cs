namespace Concrete.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Telefone",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ddd = c.String(nullable: false, maxLength: 100, unicode: false),
                        UsuarioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        DataUltimoLogin = c.DateTime(nullable: false),
                        Token = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telefone", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Telefone", new[] { "UsuarioId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Telefone");
        }
    }
}
