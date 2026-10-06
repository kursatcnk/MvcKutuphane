using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;
using MvcKutuphane.Models.Entity;
using PagedList.Mvc;
using PagedList;
namespace MvcKutuphane.Controllers
{
    public class UyelerController : Controller
    {
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TblUyelers.ToList();
            var degerler = db.TblUyelers.ToList().ToPagedList(sayfa, 3);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TblUyeler p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            p.sifre = Sifreleme.Ozetle(p.sifre ?? "");
            db.TblUyelers.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TblUyelers.Find(id);
            db.TblUyelers.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TblUyelers.Find(id);
            return View("UyeGetir", uye);
        }
        public ActionResult UyeGuncelle(TblUyeler p)
        {
            var uye = db.TblUyelers.Find(p.id);
            uye.ad = p.ad;
            uye.soyad = p.soyad;
            uye.mail = p.mail;
            uye.kullaniciAdi = p.kullaniciAdi;
            if (!string.IsNullOrWhiteSpace(p.sifre)) uye.sifre = Sifreleme.Ozetle(p.sifre);
            uye.telefon = p.telefon;
            uye.okul = p.okul;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapGecmisi(int id)
        {
            var kitap = db.TblHarekets.Where(x => x.uye == id).ToList();
            var uyead = db.TblUyelers.Where(x => x.id == id).Select(y => y.ad + " " + y.soyad).FirstOrDefault();
            ViewBag.uyeAd = uyead;
            return View(kitap);
        }
    }
}