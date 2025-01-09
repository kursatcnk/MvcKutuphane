using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class DuyuruController : Controller
    {
       DbMvcKutuphaneEntities db= new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblDuyurulars.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TblDuyurular p)
        {
            db.TblDuyurulars.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru=db.TblDuyurulars.Find(id);
            db.TblDuyurulars.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DuyuruDetay(TblDuyurular p)
        {
            var duyuru = db.TblDuyurulars.Find(p.id);
            return View("DuyuruDetay",duyuru);      

        }
        public ActionResult DuyuruGuncelle(TblDuyurular p)
        {
            var duyuru = db.TblDuyurulars.Find(p.id);
            duyuru.kategori = p.kategori;
            duyuru.icerik = p.icerik;
            duyuru.tarih = p.tarih;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}