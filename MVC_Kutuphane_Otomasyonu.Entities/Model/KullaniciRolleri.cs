using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(KullaniciRolleriValidator))]

    public class KullaniciRolleri
    {
        public int Id { get; set; }

        // FK kolonları
        public int KullaniciId { get; set; }
        public int RolId { get; set; }

        // Navigations
        public virtual Kullanicilar Kullanici { get; set; }
        public virtual Roller Rol { get; set; }
    }
}
