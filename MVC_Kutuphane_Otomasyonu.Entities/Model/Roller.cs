using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    public class Roller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rol alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "Rol alanı en fazla 50 karakter olabilir!")]
        public string Rol { get; set; }

        public virtual ICollection<KullaniciRolleri> KullaniciRolleri { get; set; }
            = new HashSet<KullaniciRolleri>();
    }
}
