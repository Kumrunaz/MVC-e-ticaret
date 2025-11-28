using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(DuyurularValidator))]
    public class Duyurular
    {
    public int Id { get; set; }
        

    public string Baslık { get; set; }

    public string Duyuru { get; set; }

    public string Aciklama { get; set; }

    public DateTime Tarih {  get; set; }

    
    }
}
