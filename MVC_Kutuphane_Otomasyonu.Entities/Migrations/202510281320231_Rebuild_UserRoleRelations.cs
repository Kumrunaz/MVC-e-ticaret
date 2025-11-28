namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Rebuild_UserRoleRelations : DbMigration
    {
        public override void Up()
        {
            // --- Eski FK'leri güvenle kaldır ---
            Sql(@"
IF OBJECT_ID(N'[dbo].[FK_dbo.KullaniciRolleri_dbo.Roller_RolId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[KullaniciRolleri] DROP CONSTRAINT [FK_dbo.KullaniciRolleri_dbo.Roller_RolId];
IF OBJECT_ID(N'[dbo].[FK_dbo.KullaniciRolleri_dbo.Kullanicilar_KullaniciId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[KullaniciRolleri] DROP CONSTRAINT [FK_dbo.KullaniciRolleri_dbo.Kullanicilar_KullaniciId];
");

            // --- Türkçe karakterli kolon & indeks düzeltmeleri (önceden yaşanan hata için) ---
            Sql(@"
IF COL_LENGTH('dbo.KullaniciHareketleri', 'KullanıcıId') IS NOT NULL
    EXEC sp_rename N'dbo.KullaniciHareketleri.KullanıcıId', N'KullaniciId', 'COLUMN';

IF EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_KullanıcıId'
      AND object_id = OBJECT_ID(N'dbo.KullaniciHareketleri')
)
    DROP INDEX [IX_KullanıcıId] ON [dbo].[KullaniciHareketleri];

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_KullaniciId'
      AND object_id = OBJECT_ID(N'dbo.KullaniciHareketleri')
)
    CREATE INDEX [IX_KullaniciId] ON [dbo].[KullaniciHareketleri]([KullaniciId]);
");

            // --- TIP DÜZELTMESİ: KullaniciRolleri.RolId & KullaniciId -> int NOT NULL ---
            // Eğer bu kolonlar NVARCHAR vb. ise önce dönüştür (gerekirse veri temizliği yap).
            // Örnek dönüşüm (sayı olmayanları NULL yapar) — ihtiyacın varsa aç:
            // Sql("UPDATE dbo.KullaniciRolleri SET RolId = TRY_CONVERT(int, RolId)");
            // Sql("UPDATE dbo.KullaniciRolleri SET KullaniciId = TRY_CONVERT(int, KullaniciId)");

            AlterColumn("dbo.KullaniciRolleri", "RolId", c => c.Int(nullable: false));
            AlterColumn("dbo.KullaniciRolleri", "KullaniciId", c => c.Int(nullable: false));

            // --- İndeksler (yoksa oluştur) ---
            Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_KullaniciRolleri_RolId' AND object_id = OBJECT_ID(N'dbo.KullaniciRolleri'))
    CREATE INDEX [IX_KullaniciRolleri_RolId] ON [dbo].[KullaniciRolleri]([RolId]);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_KullaniciRolleri_KullaniciId' AND object_id = OBJECT_ID(N'dbo.KullaniciRolleri'))
    CREATE INDEX [IX_KullaniciRolleri_KullaniciId] ON [dbo].[KullaniciRolleri]([KullaniciId]);
");

            // --- Kitaplar: GuncellenmeTarihi kolonu (İng. karakterle) ---
            Sql(@"
IF COL_LENGTH('dbo.Kitaplar','GuncellenmeTarihi') IS NULL
    ALTER TABLE [dbo].[Kitaplar] ADD [GuncellenmeTarihi] DATETIME NOT NULL DEFAULT(GETDATE());
");

            // --- KullaniciHareketleri.Aciklama uzunluk ayarı ---
            AlterColumn("dbo.KullaniciHareketleri", "Aciklama", c => c.String(maxLength: 500));

            // --- FK'leri tekrar ekle (Rol -> NoAction, Kullanici -> Cascade) ---
            AddForeignKey("dbo.KullaniciRolleri", "RolId", "dbo.Roller", "Id", cascadeDelete: false);
            AddForeignKey("dbo.KullaniciRolleri", "KullaniciId", "dbo.Kullanicilar", "Id", cascadeDelete: true);

            // --- Eski Türkçe kolon varsa kaldır ---
            Sql(@"
IF COL_LENGTH('dbo.Kitaplar','GüncellenmeTarihi') IS NOT NULL
    ALTER TABLE [dbo].[Kitaplar] DROP COLUMN [GüncellenmeTarihi];
");
        }

        public override void Down()
        {
            // Eski kolonu geri ekle, yenisini kaldır
            Sql(@"
IF COL_LENGTH('dbo.Kitaplar','GüncellenmeTarihi') IS NULL
    ALTER TABLE [dbo].[Kitaplar] ADD [GüncellenmeTarihi] DATETIME NOT NULL DEFAULT(GETDATE());
");
            Sql(@"
IF COL_LENGTH('dbo.Kitaplar','GuncellenmeTarihi') IS NOT NULL
    ALTER TABLE [dbo].[Kitaplar] DROP COLUMN [GuncellenmeTarihi];
");

            // Aciklama'yı eski haline (maxlength olmadan) çevir
            AlterColumn("dbo.KullaniciHareketleri", "Aciklama", c => c.String());

            // İndeksleri kaldır
            Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_KullaniciRolleri_RolId' AND object_id = OBJECT_ID(N'dbo.KullaniciRolleri'))
    DROP INDEX [IX_KullaniciRolleri_RolId] ON [dbo].[KullaniciRolleri];

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_KullaniciRolleri_KullaniciId' AND object_id = OBJECT_ID(N'dbo.KullaniciRolleri'))
    DROP INDEX [IX_KullaniciRolleri_KullaniciId] ON [dbo].[KullaniciRolleri];
");

            // FK'leri kaldır
            DropForeignKey("dbo.KullaniciRolleri", "KullaniciId", "dbo.Kullanicilar");
            DropForeignKey("dbo.KullaniciRolleri", "RolId", "dbo.Roller");

            // Kolon tiplerini geri döndürmek istersen (ör. string):
            // AlterColumn("dbo.KullaniciRolleri", "KullaniciId", c => c.String());
            // AlterColumn("dbo.KullaniciRolleri", "RolId", c => c.String());

            // Türkçe karakterli eski isim/indeks geri dönüşleri (gerekirse):
            Sql(@"
IF EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_KullaniciId'
      AND object_id = OBJECT_ID(N'dbo.KullaniciHareketleri')
)
    DROP INDEX [IX_KullaniciId] ON [dbo].[KullaniciHareketleri];

IF COL_LENGTH('dbo.KullaniciHareketleri', 'KullaniciId') IS NOT NULL
    EXEC sp_rename N'dbo.KullaniciHareketleri.KullaniciId', N'KullanıcıId', 'COLUMN';

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_KullanıcıId'
      AND object_id = OBJECT_ID(N'dbo.KullaniciHareketleri')
)
    CREATE INDEX [IX_KullanıcıId] ON [dbo].[KullaniciHareketleri]([KullanıcıId]);
");
        }
    }
}
