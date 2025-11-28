using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MVC_Kutuphane_Otomasyonu.Entities.Model;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class KullaniciRolleriMap : EntityTypeConfiguration<KullaniciRolleri>
    {
        public KullaniciRolleriMap()
        {
            ToTable("KullaniciRolleri");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // KullaniciRolleri -> Kullanicilar
            HasRequired(x => x.Kullanici)
                .WithMany(k => k.KullaniciRolleri)
                .HasForeignKey(x => x.KullaniciId)
                .WillCascadeOnDelete(true);

            // KullaniciRolleri -> Roller
            HasRequired(x => x.Rol)
                .WithMany(r => r.KullaniciRolleri)
                .HasForeignKey(x => x.RolId)
                .WillCascadeOnDelete(false);
        }
    }
}
