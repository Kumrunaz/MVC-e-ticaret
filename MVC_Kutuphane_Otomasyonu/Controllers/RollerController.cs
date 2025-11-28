using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    //[Authorize(Roles ="Admin")]
    [AllowAnonymous]
    public class RollerController : Controller
    {
        // GET: Roller

        KutuphaneContext context=new KutuphaneContext();
        RollerDAL rollerDAL= new RollerDAL();
        public ActionResult Index()
        {
            var model=rollerDAL.GetAll(context);
            return View(model);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Roller entity)
        {
            // DEBUG: model bindi mi, Rol ne geliyor?
            System.Diagnostics.Debug.WriteLine(
                "EKLE POST -> entity null mu? " + (entity == null ? "EVET" : "HAYIR") +
                " | Rol değeri = '" + (entity?.Rol ?? "NULL") + "'"
            );

            if (!ModelState.IsValid)
            {
                // Model hatalarını da yazalım
                foreach (var kvp in ModelState)
                {
                    foreach (var err in kvp.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"MODELSTATE ERROR -> Key={kvp.Key}, Error={err.ErrorMessage}"
                        );
                    }
                }

                return View(entity);
            }

            // Save'den HEMEN ÖNCE tekrar kontrol
            System.Diagnostics.Debug.WriteLine("KAYDEDİLİYOR -> Rol = '" + (entity.Rol ?? "NULL") + "'");

            rollerDAL.InsertorUpdate(context, entity);
            rollerDAL.Save(context);

            return RedirectToAction("Index");
        }


        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Id değeri girilmedi");

            }
            var model = rollerDAL.GetByFiltre(context,x=>x.Id==id);
            return View(model); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Roller entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }

            rollerDAL.InsertorUpdate(context, entity);
            rollerDAL.Save(context);

            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            rollerDAL.Delete(context, x => x.Id == id);
            rollerDAL.Save(context);
            return RedirectToAction("Index");
        }


    }
}