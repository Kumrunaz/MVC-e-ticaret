namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Duyurulars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslık = c.String(),
                        Duyuru = c.String(),
                        Aciklama = c.Int(nullable: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmanetKitaplars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UyeId = c.String(),
                        KitapId = c.String(),
                        KitapSayısı = c.Int(nullable: false),
                        KitapAldigiTarih = c.Int(nullable: false),
                        KitapIadeTarihi = c.Int(nullable: false),
                        Kitaplar_Id = c.Int(),
                        uyeler_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kitaplars", t => t.Kitaplar_Id)
                .ForeignKey("dbo.Uyelers", t => t.uyeler_Id)
                .Index(t => t.Kitaplar_Id)
                .Index(t => t.uyeler_Id);
            
            CreateTable(
                "dbo.Kitaplars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarkodNo = c.String(),
                        KitapTuruId = c.Int(nullable: false),
                        KitapAdı = c.String(),
                        Yazarı = c.String(),
                        YayınEvi = c.String(),
                        StokAdedi = c.String(),
                        SayfaSayısı = c.String(),
                        Aciklama = c.String(),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        GüncellenmeTarihi = c.DateTime(nullable: false),
                        SilindiMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uyelers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdiSoyadi = c.String(),
                        Telefon = c.String(),
                        Adres = c.String(),
                        EMail = c.String(),
                        Resim = c.String(),
                        OkunanKitapSayisi = c.Int(nullable: false),
                        KayıtTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hakkımızda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icerik = c.String(),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.İletisim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdiSoyadi = c.String(),
                        Email = c.String(),
                        Baslik = c.String(),
                        Mesaj = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapHareketleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullanıcıId = c.Int(nullable: false),
                        UyeId = c.Int(nullable: false),
                        KitapId = c.Int(nullable: false),
                        YapilanIslem = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapKayıtHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullanıcıId = c.Int(nullable: false),
                        KitapId = c.Int(nullable: false),
                        YapilanIslem = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapTurleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KitapTuru = c.String(),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KullanıcıHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullanıcıId = c.Int(nullable: false),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kullanıcılar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullanıcıId = c.String(),
                        Sifre = c.String(),
                        AdiSoyadi = c.String(),
                        Telefon = c.String(),
                        Adres = c.String(),
                        EMail = c.String(),
                        KayıtTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rollers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rol = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmanetKitaplars", "uyeler_Id", "dbo.Uyelers");
            DropForeignKey("dbo.EmanetKitaplars", "Kitaplar_Id", "dbo.Kitaplars");
            DropIndex("dbo.EmanetKitaplars", new[] { "uyeler_Id" });
            DropIndex("dbo.EmanetKitaplars", new[] { "Kitaplar_Id" });
            DropTable("dbo.Rollers");
            DropTable("dbo.Kullanıcılar");
            DropTable("dbo.KullanıcıHareketleri");
            DropTable("dbo.KitapTurleris");
            DropTable("dbo.KitapKayıtHareketleri");
            DropTable("dbo.KitapHareketleris");
            DropTable("dbo.İletisim");
            DropTable("dbo.Hakkımızda");
            DropTable("dbo.Uyelers");
            DropTable("dbo.Kitaplars");
            DropTable("dbo.EmanetKitaplars");
            DropTable("dbo.Duyurulars");
        }
    }
}
