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
    [Authorize(Roles = "Admin")]
    public class KitaplarController : Controller
    {
        // GET: Kitaplar
        KutuphaneContext context = new KutuphaneContext();
        KitaplarDAL KitaplarDAL = new KitaplarDAL();

        
        public ActionResult Index()
        {
            var model = KitaplarDAL.GetAll(context, null, "KitapTurleri");
            return View(model);
        }
        public ActionResult Ekle()
        {
            ViewBag.Liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Kitaplar entity)
        {
            if (ModelState.IsValid)
            {
                KitaplarDAL.InsertorUpdate(context, entity);
                KitaplarDAL.Save(context);
                return RedirectToAction("Index");
            }
            ViewBag.Liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            return View(entity);
        }
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            var model = KitaplarDAL.GetByFiltre(context, x => x.Id == id, "KitapTurleri");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kitaplar entity)
        {
            if (ModelState.IsValid)
            {
                KitaplarDAL.InsertorUpdate(context, entity);
                KitaplarDAL.Save(context);
                return RedirectToAction("Index");
            }
            ViewBag.Liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            return View(entity);
        }

        public ActionResult Detay(int? id)
        {
            var model = KitaplarDAL.GetByFiltre(context, x => x.Id == id, "KitapTurleri");
            return View(model);
        }

        public ActionResult Sil(int? id)
        {
            if (id==null)
            {
                return HttpNotFound() ;
            }
            KitaplarDAL.Delete(context,x=>x.Id == id);
            KitaplarDAL.Save(context);
            return RedirectToAction("Index");
        }



    }
}