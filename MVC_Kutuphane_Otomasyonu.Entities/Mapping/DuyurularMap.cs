using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class DuyurularMap : EntityTypeConfiguration<Duyurular>
    {
        public DuyurularMap()
        {
            ToTable("Duyurular");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); //Otomatik artan sayı 

            Property(x => x.Baslık)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(150);

            Property(x => x.Duyuru)
                .IsRequired()
                .HasMaxLength(500);

            Property(x => x.Aciklama)
                .HasMaxLength(5000);

            Property(x => x.Id).HasColumnName("Id"); // KOlon Adlandırma
            Property(x => x.Baslık).HasColumnName("Baslik");
            Property(x => x.Duyuru).HasColumnName("Duyuru");
            Property(x => x.Aciklama).HasColumnName("Aciklama");
            Property(x => x.Tarih).HasColumnName("Tarih");
        }

    
    }
}