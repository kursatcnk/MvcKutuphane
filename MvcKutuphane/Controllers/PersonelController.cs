using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblPersonels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TblPersonel p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TblPersonels.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(int id)
        {
            var personel = db.TblPersonels.Find(id);
            db.TblPersonels.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var pers = db.TblPersonels.Find(id);
            return View("PersonelGetir", pers);
        }
        public ActionResult PersonelGuncelle(TblPersonel p)
        {
            var prs = db.TblPersonels.Find(p.id);
            prs.personel = p.personel;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}