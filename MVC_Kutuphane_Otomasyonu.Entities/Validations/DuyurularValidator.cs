using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class DuyurularValidator:AbstractValidator<Duyurular>
    {

        public DuyurularValidator() 
        {
            RuleFor(x => x.Baslık).NotEmpty().WithMessage("Başlık alanı boş geçilemez!");
            RuleFor(x => x.Duyuru).NotEmpty().WithMessage("Duyuru alanı boş geçilemez!");
            RuleFor(x => x.Tarih).NotEmpty().WithMessage("Tarih alanı boş geçilemez!");
            RuleFor(x => x.Baslık).Length(5,150).WithMessage("Başlık alanı 5-150 karakter arası olmalıdır!");
            RuleFor(x => x.Duyuru).MaximumLength(500).WithMessage("Duyuru alanı en fazla 500 karakter arası olmalıdır!");
           



        }
    }
}
