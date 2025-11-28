using MVC_Kutuphane_Otomasyonu.Entities.Interfaces;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using MVC_Kutuphane_Otomasyonu.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.DAL
{

    public class KitapHareketlerDAL :GenericRepository<KutuphaneContext,KitapHareketleri>
    {

    }
}
