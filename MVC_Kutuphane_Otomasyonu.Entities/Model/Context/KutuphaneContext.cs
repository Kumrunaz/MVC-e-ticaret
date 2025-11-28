using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using MVC_Kutuphane_Otomasyonu.Entities.Mapping;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model.Context
{
    public class KutuphaneContext:DbContext
    {
        public KutuphaneContext() : base("Kutuphane")
        {

        }
        public DbSet<Duyurular> Duyurular { get; set; }
        public DbSet<EmanetKitaplar> EmanetKitaplar { get; set; }
        public DbSet<Hakkimizda> Hakkımızda { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<KitapHareketleri> KitapHareketleri { get; set; }
        public DbSet<KitapKayitHareketleri> KitapKayıtHareketleri { get; set; }
        public DbSet<Kitaplar> Kitaplar { get; set; }
        public DbSet<KitapTurleri> KitapTurleri { get; set; }
        public DbSet<KullaniciHareketleri> KullanıcıHareketleri { get; set; }

        public DbSet<Kullanicilar> Kullanıcılar { get; set; }
        public DbSet<Roller> Roller { get; set; }
        public DbSet<Uyeler> Uyeler { get; set; }

        public DbSet<KullaniciRolleri> KullaniciRolleri { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DuyurularMap());
            modelBuilder.Configurations.Add(new EmanetKitaplarMap());
            modelBuilder.Configurations.Add(new HakkımızdaMap());
            modelBuilder.Configurations.Add(new İletisimMap());
            modelBuilder.Configurations.Add(new KitapHareketleriMap());
            modelBuilder.Configurations.Add(new KitapKayitHareketleriMap());
            modelBuilder.Configurations.Add(new KitaplarMap());
            modelBuilder.Configurations.Add(new KitapTurleriMap());
            modelBuilder.Configurations.Add(new KullaniciHareketleriMap());
            modelBuilder.Configurations.Add(new KullanicilarMap());
            modelBuilder.Configurations.Add(new KullaniciRolleriMap());
            modelBuilder.Configurations.Add(new RollerMap());
            modelBuilder.Configurations.Add(new UyelerMap());
        }

    }
}
