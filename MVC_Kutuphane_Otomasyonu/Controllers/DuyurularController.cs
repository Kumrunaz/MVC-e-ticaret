using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Mapping;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    public class DuyurularController : Controller
    {
        // GET: Duyurular
        KutuphaneContext context = new KutuphaneContext();
        DuyurularDAL duyurularDAL = new DuyurularDAL();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DuyuruList()
        {
            var model = duyurularDAL.GetAll(context);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Duyurular model)
        {
            ModelState.Remove("Tarih");
            if (ModelState.IsValid)
            {
                model.Tarih = DateTime.Now; // otomatik tarih ekle
                context.Duyurular.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ▼▼ Modal için tek kayıt döndürür
        [HttpGet]
        public JsonResult DuyuruGetir(int id)
        {
            var model = duyurularDAL.GetByFiltre(context, x => x.Id == id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        // ▼▼ Sil (mevcut linkin GET olması nedeniyle GET bırakıldı)
        public ActionResult Sil(int id)
        {
            var duyuru = context.Duyurular.Find(id);
            if (duyuru == null)
                return HttpNotFound();

            context.Duyurular.Remove(duyuru);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ▼▼ Güncelle (AJAX POST)
        [HttpPost]
        public JsonResult Guncelle(int Id, string Baslık, string Duyuru, string Aciklama, string Tarih)
        {
            var entity = context.Duyurular.Find(Id);
            if (entity == null)
                return Json(false, JsonRequestBehavior.AllowGet);

            // Alanları güncelle
            entity.Baslık = Baslık;
            entity.Duyuru = Duyuru;
            entity.Aciklama = Aciklama;

            // Tarih gönderildiyse parse et (datetime-local için ISO formatı destekle)
            if (!string.IsNullOrWhiteSpace(Tarih))
            {
                DateTime dt;
                // Önce datetime-local 'YYYY-MM-DDTHH:mm' dene
                if (DateTime.TryParseExact(Tarih, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture,
                                           DateTimeStyles.AssumeLocal, out dt)
                    || DateTime.TryParse(Tarih, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt))
                {
                    entity.Tarih = dt;
                }
            }

            context.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SeciliSil(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Json(false, JsonRequestBehavior.AllowGet);

            var silinecekler = context.Duyurular.Where(d => ids.Contains(d.Id)).ToList();
            if (!silinecekler.Any())
                return Json(false, JsonRequestBehavior.AllowGet);

            context.Duyurular.RemoveRange(silinecekler);
            context.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }

}
