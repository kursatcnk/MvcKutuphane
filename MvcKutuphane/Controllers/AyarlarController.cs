using MvcKutuphane.Models;
using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MvcKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
        public ActionResult Admin()
        {
            var kullanici = db.TblAdmins.ToList();
            return View(kullanici);
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TblAdmin p)
        {
            p.sifre = Sifreleme.Ozetle(p.sifre ?? "");
            db.TblAdmins.Add(p);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }
        public ActionResult AdminSil(int id)
        {
            var admin=db.TblAdmins.Find(id);
            db.TblAdmins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }
        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var admin=db.TblAdmins.Find(id);
            return View("AdminGuncelle",admin);
        }
        [HttpPost]
        public ActionResult AdminGuncelle(TblAdmin p)
        {
            var admin = db.TblAdmins.Find(p.id);
            admin.kullaniciadi = p.kullaniciadi;
            // Şifre alanı boş bırakılırsa mevcut şifre değişmiyor.
            if (!string.IsNullOrWhiteSpace(p.sifre)) admin.sifre = Sifreleme.Ozetle(p.sifre);
            admin.yetki = p.yetki;
            db.SaveChanges();
            return RedirectToAction("Admin");
        }
    }
}