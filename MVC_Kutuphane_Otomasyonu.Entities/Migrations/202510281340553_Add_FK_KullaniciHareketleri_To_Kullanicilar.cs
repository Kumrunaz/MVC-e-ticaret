namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FK_KullaniciHareketleri_To_Kullanicilar : DbMigration
    {
        public override void Up()
        {
            // İndeks yoksa oluştur
            Sql(@"
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_KullaniciHareketleri_KullaniciId'
      AND object_id = OBJECT_ID(N'dbo.KullaniciHareketleri')
)
    CREATE INDEX [IX_KullaniciHareketleri_KullaniciId]
        ON [dbo].[KullaniciHareketleri]([KullaniciId]);
");

            // FK'yi ekle (cascade delete istiyorsak true)
            AddForeignKey(
                "dbo.KullaniciHareketleri",
                "KullaniciId",
                "dbo.Kullanicilar",
                "Id",
                cascadeDelete: true
            );


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KullaniciHareketleri", "KullaniciId", "dbo.Kullanicilar");
            Sql(@"
IF EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_KullaniciHareketleri_KullaniciId'
      AND object_id = OBJECT_ID(N'dbo.KullaniciHareketleri')
)
    DROP INDEX [IX_KullaniciHareketleri_KullaniciId]
        ON [dbo].[KullaniciHareketleri];
");


        }
    }
}
