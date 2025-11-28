using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(KitaplarValidator))]

    public class Kitaplar
    {
        public int Id { get; set; }

        public string BarkodNo { get; set; }

        public int KitapTuruId { get; set; }

        public string KitapAdi { get; set; }

        public string Yazari { get; set; }

        public string YayinEvi { get; set; }

        public int StokAdedi { get; set; }

        public int SayfaSayisi { get; set; }

        public string Aciklama { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;

        public DateTime GuncellemeTarihi { get; set; } = DateTime.Now; // Türkçe karakter kullanma

        public bool SilindiMi { get; set; }=false;

        // Navigation Properties
        public virtual KitapTurleri KitapTurleri { get; set; }

        public virtual ICollection<EmanetKitaplar> EmanetKitaplar { get; set; }

        public virtual ICollection<KitapHareketleri> KitapHareketleri { get; set; }

        public virtual ICollection<KitapKayitHareketleri> KitapKayitHareketleri { get; set; }
    }
}
