namespace Concrete.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "DataCriacao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "DataAtualizacao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "DataUltimoLogin", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Usuario", "Token", c => c.String(nullable: false, maxLength: 300, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Token", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Usuario", "DataUltimoLogin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Usuario", "DataAtualizacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Usuario", "DataCriacao", c => c.DateTime(nullable: false));
        }
    }
}
