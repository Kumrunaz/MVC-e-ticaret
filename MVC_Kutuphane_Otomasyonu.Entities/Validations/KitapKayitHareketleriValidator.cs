using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class KitapKayitHareketleriValidator:AbstractValidator<KitapKayitHareketleri>
    {
        public KitapKayitHareketleriValidator()
        {
            RuleFor(x => x.YapilanIslem).MaximumLength(150).WithMessage("Yapılan İşlem alanı en fazla 150 karakter olabilir!");

        }
    }
}
