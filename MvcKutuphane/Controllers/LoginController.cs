using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var bilgiler = db.TblUyelers.FirstOrDefault(x => x.mail == p.mail && x.sifre == p.sifre);
            if (bilgiler != null)
            {
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