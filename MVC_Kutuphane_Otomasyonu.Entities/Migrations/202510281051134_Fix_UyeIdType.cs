namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Fix_UyeIdType : DbMigration
    {
        public override void Up()
        {
            // Eski FK’leri güvenli şekilde kaldır
            Sql(@"
IF OBJECT_ID(N'[dbo].[FK_dbo.EmanetKitaplars_dbo.Uyelers_uyeler_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[EmanetKitaplar] DROP CONSTRAINT [FK_dbo.EmanetKitaplars_dbo.Uyelers_uyeler_Id];
IF OBJECT_ID(N'[dbo].[FK_dbo.EmanetKitaplar_dbo.Kitaplar_Kitaplar_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[EmanetKitaplar] DROP CONSTRAINT [FK_dbo.EmanetKitaplar_dbo.Kitaplar_Kitaplar_Id];
");

            DropIndex("dbo.EmanetKitaplar", new[] { "Kitaplar_Id" });
            DropIndex("dbo.EmanetKitaplar", new[] { "uyeler_Id" });

            DropColumn("dbo.EmanetKitaplar", "KitapId");
            RenameColumn(table: "dbo.EmanetKitaplar", name: "Kitaplar_Id", newName: "KitapId");

            AlterColumn("dbo.EmanetKitaplar", "UyeId", c => c.Int(nullable: false));
            AlterColumn("dbo.EmanetKitaplar", "KitapId", c => c.Int(nullable: false));
            AlterColumn("dbo.EmanetKitaplar", "KitapAldigiTarih", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmanetKitaplar", "KitapIadeTarihi", c => c.DateTime(nullable: false));

            AlterColumn("dbo.Kitaplar", "StokAdedi", c => c.Int(nullable: false));
            AlterColumn("dbo.Kitaplar", "SayfaSayisi", c => c.Int(nullable: false));

            // <<< DÜZELTME: İndeksler zaten varsa oluşturma >>>
            Sql(@"
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_KitapId'
      AND object_id = OBJECT_ID('dbo.EmanetKitaplar')
)
    CREATE INDEX [IX_KitapId] ON [dbo].[EmanetKitaplar]([KitapId]);

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_UyeId'
      AND object_id = OBJECT_ID('dbo.EmanetKitaplar')
)
    CREATE INDEX [IX_UyeId] ON [dbo].[EmanetKitaplar]([UyeId]);
");

            // Diğer tablolar (bunlarda çakışma yoksa EF'in CreateIndex'i kalsın)
            CreateIndex("dbo.KitapHareketleri", "KitapId");
            CreateIndex("dbo.KitapKayitHareketleri", "KitapId");

            AddForeignKey("dbo.KitapHareketleri", "KitapId", "dbo.Kitaplar", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KitapKayitHareketleri", "KitapId", "dbo.Kitaplar", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmanetKitaplar", "KitapId", "dbo.Kitaplar", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmanetKitaplar", "UyeId", "dbo.Uyeler", "Id", cascadeDelete: false);

            DropColumn("dbo.EmanetKitaplar", "uyeler_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.EmanetKitaplar", "uyeler_Id", c => c.Int());
            DropForeignKey("dbo.EmanetKitaplar", "UyeId", "dbo.Uyeler");
            DropForeignKey("dbo.EmanetKitaplar", "KitapId", "dbo.Kitaplar");
            DropForeignKey("dbo.KitapKayitHareketleri", "KitapId", "dbo.Kitaplar");
            DropForeignKey("dbo.KitapHareketleri", "KitapId", "dbo.Kitaplar");
            DropIndex("dbo.EmanetKitaplar", new[] { "UyeId" });
            DropIndex("dbo.KitapKayitHareketleri", new[] { "KitapId" });
            DropIndex("dbo.KitapHareketleri", new[] { "KitapId" });
            DropIndex("dbo.EmanetKitaplar", new[] { "KitapId" });
            AlterColumn("dbo.Kitaplar", "SayfaSayisi", c => c.String());
            AlterColumn("dbo.Kitaplar", "StokAdedi", c => c.String());
            AlterColumn("dbo.EmanetKitaplar", "KitapIadeTarihi", c => c.Int(nullable: false));
            AlterColumn("dbo.EmanetKitaplar", "KitapAldigiTarih", c => c.Int(nullable: false));
            AlterColumn("dbo.EmanetKitaplar", "KitapId", c => c.String());
            AlterColumn("dbo.EmanetKitaplar", "UyeId", c => c.String());
            RenameColumn(table: "dbo.EmanetKitaplar", name: "KitapId", newName: "Kitaplar_Id");
            AddColumn("dbo.EmanetKitaplar", "KitapId", c => c.String());
            CreateIndex("dbo.EmanetKitaplar", "uyeler_Id");
            CreateIndex("dbo.EmanetKitaplar", "Kitaplar_Id");
            AddForeignKey("dbo.EmanetKitaplar", "Kitaplar_Id", "dbo.Kitaplar", "Id");
            AddForeignKey("dbo.EmanetKitaplar", "uyeler_Id", "dbo.Uyeler", "Id");
        }
    }
}
