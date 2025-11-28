using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(KitapTurleriValidator))]



    public class KitapTurleri
    {
        public int Id { get; set; }
        public string KitapTuru { get; set; }
        public string Aciklama { get; set; }


        public List<Kitaplar> Kitaplar { get; set; }  //Coğul adlandırma
    }
}
