using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Mapping;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using MVC_Kutuphane_Otomasyonu.Entities.Model.ViewModel;
using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    [Authorize(Roles = "Admin,Moderatör")]
    public class KullanicilarController : Controller
    {
        KutuphaneContext context = new KutuphaneContext();
        KullanicilarDAL kullanıcılarDAL = new KullanicilarDAL();
        KullaniciRolleriDAL kullaniciRolleriDAL = new KullaniciRolleriDAL();
        RollerDAL rollerDAL = new RollerDAL();

        // Anti-forgery cookie’sini temizlemek için yardımcı
        private void ClearAntiForgeryCookie()
        {
            var cookies = Request.Cookies.AllKeys;
            foreach (var key in cookies)
            {
                if (key != null && key.Contains("__RequestVerificationToken"))
                {
                    var expired = new HttpCookie(key) { Expires = DateTime.UtcNow.AddDays(-1) };
                    Response.Cookies.Add(expired);
                }
            }
        }


        public ActionResult Index()
        {
            var model = kullanıcılarDAL.GetAll(context);
            return View(model);
        }
        public ActionResult Index2()
        {
            var kullanicilar = kullanıcılarDAL.GetAll(context, tbl: "KullaniciRolleri");
            var roller = rollerDAL.GetAll(context);

            var model = new KullaniciRolViewModel
            {
                Kullanicilar = kullanicilar,
                Roller = roller
            };

            return View(model);
        }
        public ActionResult Ekle()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Kullanicilar entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }

            entity.KayitTarihi = entity.KayitTarihi == default(DateTime)
                ? DateTime.Now
                : entity.KayitTarihi;

            if (string.IsNullOrWhiteSpace(entity.Aciklama))
            {
                entity.Aciklama = "Yönetici panelinden eklendi.";
            }

            try
            {
                context.Kullanıcılar.Add(entity);
                context.SaveChanges();

                TempData["Mesaj"] = "Kullanıcı eklendi.";
                return RedirectToAction("Index2");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(entity);
        }

        [HttpGet]
        public ActionResult Duzenle(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = context.Kullanıcılar.Find(id);
            if (model == null) return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kullanicilar entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }

            var mevcut = context.Kullanıcılar.Find(entity.Id);
            if (mevcut == null) return HttpNotFound();

            mevcut.AdiSoyadi = entity.AdiSoyadi;
            mevcut.Telefon = entity.Telefon;
            mevcut.Adres = entity.Adres;
            mevcut.Email = entity.Email;
            mevcut.KullaniciAdi = entity.KullaniciAdi;
            mevcut.Sifre = entity.Sifre;
            mevcut.KayitTarihi = entity.KayitTarihi == default(DateTime)
                ? mevcut.KayitTarihi
                : entity.KayitTarihi;
            mevcut.Aciklama = string.IsNullOrWhiteSpace(entity.Aciklama)
                ? mevcut.Aciklama
                : entity.Aciklama;

            context.SaveChanges();
            TempData["Mesaj"] = "Kullanıcı güncellendi.";
            return RedirectToAction("Index2");
        }

        [HttpGet]
        public ActionResult Sil(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = context.Kullanıcılar.Find(id);
            if (model == null) return HttpNotFound();

            context.Kullanıcılar.Remove(model);
            context.SaveChanges();

            TempData["Mesaj"] = "Kullanıcı silindi.";
            return RedirectToAction("Index2");
        }

        public ActionResult KRolleri(int id)
        {
            var model = kullaniciRolleriDAL.GetAll(context, x => x.KullaniciId == id, "Rol");
            if (model != null)
            {
                ViewBag.KullaniciId = id;
                return View(model);
            }
            return HttpNotFound();
        }

        // ---- LOGIN ----
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();

            return View();   // burada cookie/hidden input'u framework üretsin
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Kullanicilar entity)
        {
            if (User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();

            var email = (entity?.Email ?? "").Trim();
            var sifre = entity?.Sifre ?? "";

            var model = kullanıcılarDAL.GetByFiltre(context, x => x.Email == email && x.Sifre == sifre);

            if (model != null)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index2", "KitapTurleri");
            }
            ClearAntiForgeryCookie();
            ViewBag.error = "Kullanıcı adı veya şifre yanlış.";
            return View(entity);
        }

        // ---- LOGOUT (istersen kullanırsın, aksi halde dokunma) ----
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            ClearAntiForgeryCookie();
            return RedirectToAction("Login");
        }

        // ---- KAYIT OL ----
        [HttpGet]
        [AllowAnonymous]
        public ActionResult KayitOl() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult KayitOl(Kullanicilar entity, string sifreTekrar, bool kabul)
        {
            if (!ModelState.IsValid) return View(entity);

            if (entity.Sifre != sifreTekrar)
            {
                ViewBag.sifreError = "Şifreler Uyuşmuyor!";
                return View(entity);
            }

            if (!kabul)
            {
                ViewBag.kabulError = "Lütfen Şartları Kabul Ediniz.";
                return View(entity);
            }

            entity.KayitTarihi = DateTime.Now;
            kullanıcılarDAL.InsertorUpdate(context, entity);
            kullanıcılarDAL.Save(context);
            return RedirectToAction("Login");
        }

        // ---- ŞİFREMİ UNUTTUM ----
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SifremiUnuttum() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SifremiUnuttum(Kullanicilar entity)
        {
            if (string.IsNullOrWhiteSpace(entity?.Email))
            {
                ViewBag.hata = "Lütfen e-mail adresinizi girin.";
                return View(entity);
            }

            var model = kullanıcılarDAL.GetByFiltre(context, x => x.Email == entity.Email.Trim());
            if (model == null)
            {
                ViewBag.hata = "Böyle bir e-mail adresi bulunamadı.";
                return View(entity);
            }

            var yeniSifre = Guid.NewGuid().ToString("N").Substring(0, 8);
            model.Sifre = yeniSifre; // PROD'da hashleyin
            kullanıcılarDAL.Save(context);

            try
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                using (var mail = new MailMessage())
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;

                    // Bu bilgileri web.config'e taşıyın
                    var username = "kumrunaztunaboylu145@gmail.com";
                    var appPassword = "grwg qrrd cbrl nkog";
                    client.Credentials = new NetworkCredential(username, appPassword);

                    mail.From = new MailAddress(username, "Şifre Sıfırlama");
                    mail.To.Add(model.Email);
                    mail.Subject = "Şifre Değiştirme İsteği";
                    mail.IsBodyHtml = true;
                    mail.Body = $"Merhaba {model.AdiSoyadi}<br/>" +
                                $"Kullanıcı Adınız: {model.KullaniciAdi}<br/>" +
                                $"Yeni Şifreniz: <b>{yeniSifre}</b>";

                    client.Send(mail);
                }

                TempData["ok"] = "Yeni şifreniz e-posta adresinize gönderildi.";
                return RedirectToAction("Login");
            }
            catch (SmtpException ex)
            {
                ViewBag.hata = "E-posta gönderilemedi: " + ex.Message +
                               (ex.InnerException != null ? " (" + ex.InnerException.Message + ")" : "");
                return View(entity);
            }
        }
    }
}
