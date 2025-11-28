using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MVC_Kutuphane_Otomasyonu.Entities.Model;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class KullaniciHareketleriMap : EntityTypeConfiguration<KullaniciHareketleri>
    {
        public KullaniciHareketleriMap()
        {
            ToTable("KullaniciHareketleri");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Aciklama).HasMaxLength(500);

            HasRequired(x => x.Kullanici)
                .WithMany(k => k.KullaniciHareketleri)
                .HasForeignKey(x => x.KullaniciId)
                .WillCascadeOnDelete(true);
        }
    }
}
