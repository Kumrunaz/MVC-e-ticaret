namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoller : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roller", "Rol", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roller", "Rol", c => c.String(nullable: false));
        }
    }
}
