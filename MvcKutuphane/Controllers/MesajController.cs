using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class MesajController : Controller
    {
        DbMvcKutuphaneEntities db=new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            
            var uyemail = (string)Session["Mail"].ToString(); // Sessiondan hangi email ile oturum açıldıysa onu çekiyorum
            var mesajlar = db.TblMesajlars.Where(x => x.alici == uyemail.ToString()).ToList(); // sadece alicinin giriş yapılan mail ile aynı olduğu mesajları getiriyorum.

            return View(mesajlar);
        }
        public ActionResult GidenMesaj()
        {

            var uyemail = (string)Session["Mail"].ToString(); // Sessiondan hangi email ile oturum açıldıysa onu çekiyorum
            var mesajlar = db.TblMesajlars.Where(x => x.gonderen == uyemail.ToString()).ToList(); // sadece alicinin giriş yapılan mail ile aynı olduğu mesajları getiriyorum.

            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(TblMesajlar t)
        {
            var uyemail = (string)Session["Mail"].ToString();
            t.gonderen = uyemail.ToString();
            t.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblMesajlars.Add(t);
            db.SaveChanges();
            return RedirectToAction("GidenMesaj","Mesaj");

        }
        public PartialViewResult MesajSideBarPartial()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var gelenSayisi = db.TblMesajlars.Where(x => x.alici == uyemail).Count();
            ViewBag.m1 = gelenSayisi;
            var gidenSayisi = db.TblMesajlars.Where(x => x.gonderen == uyemail).Count();
            ViewBag.m2 = gidenSayisi;
            return PartialView();
        }
      

    }
}