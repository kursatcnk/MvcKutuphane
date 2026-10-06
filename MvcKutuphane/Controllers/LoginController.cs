using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
       
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();

        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]

        public ActionResult GirisYap(TblUyeler p)
        {
            // Şifre sorguda değil, burada özetle karşılaştırılıyor; eski düz metin kayıt ilk girişte özetleniyor.
            var bilgiler = db.TblUyelers.FirstOrDefault(x => x.mail == p.mail);
            if (bilgiler != null && Sifreleme.Dogrula(p.sifre, bilgiler.sifre))
            {
                if (!Sifreleme.YeniBicimde(bilgiler.sifre))
                {
                    bilgiler.sifre = Sifreleme.Ozetle(p.sifre);
                    db.SaveChanges();
                }
                FormsAuthentication.SetAuthCookie(bilgiler.mail, false);
                Session["Mail"] = bilgiler.mail.ToString();
                //TempData["Id"] = bilgiler.id.ToString();
                //TempData["Ad"] = bilgiler.ad.ToString();
                //TempData["Soyad"] = bilgiler.soyad.ToString();
                //TempData["KullaniciAdi"] = bilgiler.kullaniciAdi.ToString();
                //TempData["Sifre"] = bilgiler.sifre.ToString();
                //TempData["Okul"] = bilgiler.okul.ToString();
                return RedirectToAction("Index", "Panel");
            }
            else
            {
                return View();
            }

        }
    }
}