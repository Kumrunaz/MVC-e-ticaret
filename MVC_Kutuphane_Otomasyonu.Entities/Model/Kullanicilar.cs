using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(KullanicilarValidator))]

    public class Kullanicilar
    {
        public int Id { get; set; }

        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string Aciklama { get; set; }

        // Navigations
        public virtual ICollection<KullaniciHareketleri> KullaniciHareketleri { get; set; }
            = new HashSet<KullaniciHareketleri>();

        public virtual ICollection<KullaniciRolleri> KullaniciRolleri { get; set; }
            = new HashSet<KullaniciRolleri>();
    }
}
