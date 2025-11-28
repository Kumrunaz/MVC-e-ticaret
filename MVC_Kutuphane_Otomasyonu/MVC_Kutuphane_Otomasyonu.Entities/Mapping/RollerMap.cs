using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class RollerMap : EntityTypeConfiguration<Roller>
    {
        public RollerMap()
        {
            ToTable("Roller");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Rol)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}

