using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    public class KullaniciRolleriController : Controller
    {
        // GET: KullaniciRolleri
        KutuphaneContext context = new KutuphaneContext();
        KullaniciRolleriDAL kullaniciRolleriDAL = new KullaniciRolleriDAL();
        RollerDAL rollerDAL = new RollerDAL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Ekle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Kullanıcı Id değeri girilmedi!");
            }

            var model = new KullaniciRolleri
            {
                KullaniciId = id.Value
            };

            ViewBag.Liste = new SelectList(rollerDAL.GetAll(context), "Id", "Rol");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(KullaniciRolleri entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Liste = new SelectList(rollerDAL.GetAll(context), "Id", "Rol", entity.RolId);
                return View(entity);
            }

            entity.Kullanici = null;
            entity.Rol = null;

            kullaniciRolleriDAL.InsertorUpdate(context, entity);
            kullaniciRolleriDAL.Save(context);

            TempData["Mesaj"] = "Kullanıcıya rol eklendi.";
            return RedirectToAction("KRolleri", "Kullanicilar", new { id = entity.KullaniciId });
        }
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Rol kaydı bulunamadı.");
            }

            var model = context.Set<KullaniciRolleri>()
                .Include("Kullanici")
                .FirstOrDefault(x => x.Id == id.Value);
            if (model == null)
            {
                return HttpNotFound("Rol kaydı bulunamadı.");
            }

            ViewBag.KullaniciAdi = model.Kullanici?.KullaniciAdi;
            ViewBag.Liste = new SelectList(rollerDAL.GetAll(context), "Id", "Rol", model.RolId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(KullaniciRolleri entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Liste = new SelectList(rollerDAL.GetAll(context), "Id", "Rol", entity.RolId);
                var current = context.Set<KullaniciRolleri>()
                    .Include("Kullanici")
                    .FirstOrDefault(x => x.Id == entity.Id);
                ViewBag.KullaniciAdi = current?.Kullanici?.KullaniciAdi;
                return View(entity);
            }

            var mevcut = context.Set<KullaniciRolleri>().Find(entity.Id);
            if (mevcut == null)
            {
                return HttpNotFound("Rol kaydı bulunamadı.");
            }

            mevcut.RolId = entity.RolId;
            context.SaveChanges();

            TempData["Mesaj"] = "Kullanıcı rolü güncellendi.";
            return RedirectToAction("KRolleri", "Kullanicilar", new { id = mevcut.KullaniciId });
        }

        [HttpGet]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Rol kaydı bulunamadı.");
            }

            var model = context.Set<KullaniciRolleri>().Find(id.Value);
            if (model == null)
            {
                return HttpNotFound("Rol kaydı bulunamadı.");
            }

            var kullaniciId = model.KullaniciId;
            context.Set<KullaniciRolleri>().Remove(model);
            context.SaveChanges();

            TempData["Mesaj"] = "Kullanıcı rolü silindi.";
            return RedirectToAction("KRolleri", "Kullanicilar", new { id = kullaniciId });
        }
    }
}