using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(KullaniciHareketleriValidator))]

    public class KullaniciHareketleri
    {
        public int Id { get; set; }

        // FK
        public int KullaniciId { get; set; }

        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }

        // Navigation
        public virtual Kullanicilar Kullanici { get; set; }
        public object Kullanicilar { get; internal set; }
    }
}
