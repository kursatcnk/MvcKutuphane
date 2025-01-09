using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        DbMvcKutuphaneEntities db =new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblKategoris.Where(x=>x.durum==true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(TblKategori p)
        {
           db.TblKategoris.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TblKategoris.Find(id);
            kategori.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TblKategoris.Find(id);
            return View("KategoriGetir",ktg);
        }
        public ActionResult KategoriGuncelle(TblKategori p)
        {
            var ktg=db.TblKategoris.Find(p.id);
            ktg.ad = p.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}