using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    [Authorize(Roles = "S")]
    public class OduncController : Controller
    {
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
       
        public ActionResult Index()
        {
            var degerler = db.TblHarekets.Where(x => x.islemdurum == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> uye = (from x in db.TblUyelers.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.uyeAd = uye;

            List<SelectListItem> kitap = (from x in db.TblKitaps.Where(x=>x.durum==true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.ad,
                                              Value = x.id.ToString()
                                          }).ToList();
            ViewBag.kitapAd = kitap;

            List<SelectListItem> personel = (from x in db.TblPersonels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.personel,
                                                 Value = x.id.ToString()
                                             }).ToList();
            ViewBag.personelAd = personel;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TblHareket p)
        {
            var d1 = db.TblUyelers.Where(x => x.id == p.TblUyeler.id).FirstOrDefault();
            var d2 = db.TblKitaps.Where(x => x.id == p.TblKitap.id).FirstOrDefault();
            var d3 = db.TblPersonels.Where(x => x.id == p.TblPersonel.id).FirstOrDefault();
            p.TblUyeler = d1;
            p.TblKitap = d2;
            p.TblPersonel = d3;
            db.TblHarekets.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OduncIade(TblHareket p)
        {
            var odn = db.TblHarekets.Find(p.id);
            DateTime d1 = DateTime.Parse(odn.iadetarih.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d1 - d2;
            ViewBag.dgr = d3.TotalDays;
            return View("OduncIade", odn);

        }
        public ActionResult OduncGuncelle(TblHareket p)
        {
            var odnc = db.TblHarekets.Find(p.id);
            odnc.uyegetirtarih = p.uyegetirtarih;
            odnc.islemdurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}