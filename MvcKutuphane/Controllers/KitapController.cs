using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{

    public class KitapController : Controller
    {
       DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
        public ActionResult Index(string p)
        {
            var kitaplar= from k in db.TblKitaps select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.ad.Contains(p));
            }
            return View(kitaplar.ToList());
            //var kitaplar = db.TblKitaps.Where(k => k.durum == true).ToList();
        }

        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger=(from i in db.TblKategoris.ToList()
                                        select new SelectListItem
                                        {
                                            Text=i.ad,
                                            Value=i.id.ToString()
                                        }).ToList();
            ViewBag.dgr = deger;
            List<SelectListItem> deger2 = (from i in db.TblYazars.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.ad +' ' + i.soyad,
                                               Value = i.id.ToString()


                                           }).ToList();
            ViewBag.dgr2 = deger2;         
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TblKitap p)
        {
            var ktg=db.TblKategoris.Where(x=>x.id==p.TblKategori.id).FirstOrDefault();
            var yzr=db.TblYazars.Where(y=>y.id==p.TblYazar.id).FirstOrDefault();
            p.TblKategori = ktg;
            p.TblYazar = yzr;
            db.TblKitaps.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var kitap = db.TblKitaps.Find(id);
            db.TblKitaps.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         public ActionResult KitapGetir(int id)
        {
            var ktp = db.TblKitaps.Find(id);
            List<SelectListItem> deger = (from i in db.TblKategoris.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.ad,
                                              Value = i.id.ToString()
                                          }).ToList();
            ViewBag.dgr = deger;
            List<SelectListItem> deger2 = (from i in db.TblYazars.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.ad + ' ' + i.soyad,
                                               Value = i.id.ToString()


                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View("KitapGetir", ktp);
        }
        public ActionResult KitapGuncelle(TblKitap p)
        {
            var kitap = db.TblKitaps.Find(p.id);
            kitap.ad=p.ad;
            kitap.basimtarih=p.basimtarih;
            kitap.sayfa=p.sayfa;
            kitap.durum = true;
            kitap.yayinevi=p.yayinevi;
            var ktg = db.TblKategoris.Where(x => x.id == p.TblKategori.id).FirstOrDefault();
            var yzr = db.TblYazars.Where(y => y.id == p.TblYazar.id).FirstOrDefault();
            kitap.kategori = ktg.id;
            kitap.kitapresim = p.kitapresim;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}