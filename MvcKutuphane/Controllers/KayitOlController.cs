using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {
        DbMvcKutuphaneEntities db=new DbMvcKutuphaneEntities();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TblUyeler p)
        {
            // Giriş e-postayla yapıldığı için aynı adresle ikinci üye açılmıyor.
            if (db.TblUyelers.Any(x => x.mail == p.mail))
                ModelState.AddModelError("mail", "Bu e-posta adresiyle kayıtlı bir üye var.");
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            p.sifre = Sifreleme.Ozetle(p.sifre ?? "");
            db.TblUyelers.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}