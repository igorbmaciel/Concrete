namespace Concrete.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Token", c => c.String(nullable: false, maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Token", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
