namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RenameGuncellenmeTarihiColumn : DbMigration
    {
        public override void Up()
        {
            // varsa ekleme, yoksa ekleme
            Sql(@"
IF COL_LENGTH('dbo.Kitaplar','GuncellemeTarihi') IS NULL
BEGIN
    ALTER TABLE dbo.Kitaplar ADD GuncellemeTarihi DATETIME NOT NULL CONSTRAINT DF_Kitaplar_GuncellemeTarihi DEFAULT (GETDATE());
END
");

            // varsa eski kolonu kaldır
            Sql(@"
IF COL_LENGTH('dbo.Kitaplar','GuncellenmeTarihi') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Kitaplar DROP COLUMN GuncellenmeTarihi;
END
");
        }

        public override void Down()
        {
            AddColumn("dbo.Kitaplar", "GuncellenmeTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.Kitaplar", "GuncellemeTarihi");
        }
    }
}
