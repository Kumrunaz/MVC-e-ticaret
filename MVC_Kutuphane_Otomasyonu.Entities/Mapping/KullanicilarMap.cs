using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class KullanicilarMap : EntityTypeConfiguration<Kullanicilar>
    {
        public KullanicilarMap()
        {
            ToTable("Kullanicilar");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.KullaniciAdi).IsRequired().HasMaxLength(30);
            Property(x => x.Sifre).IsRequired().HasMaxLength(15);
            Property(x => x.AdiSoyadi).IsRequired().HasMaxLength(100);
            Property(x => x.Telefon).IsRequired().HasMaxLength(20);
            Property(x => x.Adres).IsRequired().HasMaxLength(500);
            Property(x => x.Email).IsRequired().HasMaxLength(100);
            Property(x => x.Aciklama).IsRequired().HasMaxLength(100);


            


        }
    }
}
