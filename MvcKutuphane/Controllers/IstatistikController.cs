using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var deger1 = db.TblUyelers.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TblKitaps.Count();
            ViewBag.dgr2 = deger2;
            var deger3 = db.TblKitaps.Where(x => x.durum == false).Count();
            ViewBag.dgr3 = deger3;
            var deger4 = db.TblCezalars.Sum(x => x.para);
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/Resim"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

        public ActionResult LinqKart()

        {
            var deger1 = db.TblKitaps.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TblUyelers.Count();
            ViewBag.dgr2 = deger2;
            var deger3 = db.TblCezalars.Sum(x => x.para);
            ViewBag.dgr3 = deger3;
            var deger4 = db.TblKitaps.Where(x => x.durum == false).Count();
            ViewBag.dgr4 = deger4;
            var deger5 = db.TblKategoris.Count();
            ViewBag.dgr5 = deger5;
            var deger8 = db.EnFazlaKitapYazar().FirstOrDefault();
            ViewBag.dgr8 = deger8;
            var deger9 = db.TblKitaps.GroupBy(x => x.yayinevi).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.dgr9 = deger9;

            var deger11 = db.TblIletisims.Count();
            ViewBag.dgr11 = deger11;

            return View();
        }
    }
}