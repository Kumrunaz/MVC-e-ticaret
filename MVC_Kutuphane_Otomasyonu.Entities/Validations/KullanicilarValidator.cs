using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class KullanicilarValidator:AbstractValidator<Kullanicilar>
    {
        public KullanicilarValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez!");
            RuleFor(x => x.Email).MaximumLength(150).WithMessage("Email alanı en fazla 150 karakter olabilir !");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen bir mail adresi formatı giriniz");



            RuleFor(x => x.AdiSoyadi).NotEmpty().WithMessage("Adı Soyadı alanı boş geçilemez!");
            RuleFor(x => x.AdiSoyadi).MaximumLength(100).WithMessage("Adı Soyadı alanı en fazla 100 karakter olabilir !");

            RuleFor(x => x.KullaniciAdi).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez!");
            RuleFor(x => x.KullaniciAdi).MaximumLength(30).WithMessage("Kullanıcı Adı alanı en fazla 30 karakter olabilir!");

            RuleFor(x => x.Sifre).NotEmpty().WithMessage("Şifre alanı boş geçilemez!");
            RuleFor(x => x.Sifre).MaximumLength(15).WithMessage("Şifre alanı en fazla 15 karakter olabilir!");
           
            RuleFor(x => x.Telefon).NotEmpty().WithMessage("Telefon alanı boş geçilemez!");
            RuleFor(x => x.Telefon).MaximumLength(20).WithMessage("Telefon alanı en fazla 20 karakter olabilir!");
           
            RuleFor(x => x.Adres).NotEmpty().WithMessage("Adres alanı boş geçilemez!");
            RuleFor(x => x.Adres).MaximumLength(500).WithMessage("Adres alanı en fazla 500 karakter olabilir!");





        }
    }
}
