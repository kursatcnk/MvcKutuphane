using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
       DbMvcKutuphaneEntities db =new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var dgr= db.TblYazars.ToList();
            return View(dgr);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
           return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TblYazar p)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TblYazars.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult YazarSil(int id)
        {
            var yazar=db.TblYazars.Find(id);
            db.TblYazars.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TblYazars.Find(id);   
            return View("YazarGetir",yzr);
        }
        public ActionResult YazarGuncelle(TblYazar p)
        {
            var dgr=db.TblYazars.Find(p.id);
            dgr.ad=p.ad;
            dgr.soyad=p.soyad;
            dgr.detay=p.detay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarKitaplar(int id)
        {
            var degerler = db.TblKitaps.Where(x => x.yazar == id).ToList();
            var yazarad = db.TblYazars.Where(x => x.id == id).Select(z=>z.ad + " " + z.soyad).FirstOrDefault();
            ViewBag.yazarAd=yazarad;
            return View(degerler);
        }
    }
}