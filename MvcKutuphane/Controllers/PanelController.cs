using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        DbMvcKutuphaneEntities db = new DbMvcKutuphaneEntities();
        [HttpGet]

        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            //var degerler = db.TblUyelers.FirstOrDefault(x => x.mail == uyemail);
            var degerler = db.TblDuyurulars.ToList();

            var d1 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.ad).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.soyad).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.kullaniciAdi).FirstOrDefault();
            ViewBag.d3 = d3;
            var d4 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.fotograf).FirstOrDefault();
            ViewBag.d4 = d4;
            var d5 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.okul).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.telefon).FirstOrDefault();
            ViewBag.d6 = d6;
            var d7 = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.mail).FirstOrDefault();
            ViewBag.d7 = d7;

            var uyeID = db.TblUyelers.Where(x => x.mail == uyemail).Select(y => y.id).FirstOrDefault();
            var d8 = db.TblHarekets.Where(x => x.uye == uyeID).Count();
            ViewBag.d8 = d8;
            var d9 = db.TblMesajlars.Where(x => x.alici == uyemail).Count();
            ViewBag.d9 = d9;
            var d10 = db.TblMesajlars.Where(x => x.gonderen == uyemail).Count();
            ViewBag.d10 = d10;
            var d11 = db.TblDuyurulars.Count();
            ViewBag.d11 = d11;
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TblUyeler p)
        {
            var kullanici = (string)Session["Mail"]; // Session'dan kullanıcının mail adresini alıyorum 
            var uye = db.TblUyelers.FirstOrDefault(x => x.mail == kullanici);  // Veritabanında kullanıcıyı buluyorum
            // Yeni şifre yazıldıysa özetlenip kaydediliyor; boşsa eski şifre kalıyor.
            if (!string.IsNullOrWhiteSpace(p.sifre)) uye.sifre = Sifreleme.Ozetle(p.sifre);
            uye.ad = p.ad;
            uye.soyad = p.soyad;
            uye.okul = p.okul;
            uye.kullaniciAdi = p.kullaniciAdi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"]; // Session'dan kullanıcının mail adresini alıyorum 

            var id = db.TblUyelers   // Kullanıcının e-posta adresinine göre idsini çekiyorum
                        .Where(x => x.mail == kullanici.ToString())
                        .Select(z => z.id)
                        .FirstOrDefault();

            // idyi çekip idye göre tüm hareketleri sıralıyorum böylece sadece ilgili kullanıcının hareketleri geliyor
            var degerler = db.TblHarekets
                            .Where(x => x.uye == id)
                            .ToList();

            return View(degerler);
        }
        public ActionResult Duyurular()
        {
            var duyurular = db.TblDuyurulars.ToList();
            return View(duyurular);

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction("GirisYap", "Login");
        }
        public PartialViewResult DuyurularPartial()
        {
            return PartialView();
        }
        public PartialViewResult AyarlarPartial()
        {
            var kullanici = (string)Session["Mail"]; // Şu an oturumda olan kullanıcının e-posta adresini alıyorum.           
            var id = db.TblUyelers.Where(x => x.mail == kullanici).Select(y => y.id).FirstOrDefault(); // Bu e-posta adresine sahip kullanıcının veritabanındaki kimliğini buluyorum.
            var uyegetir = db.TblUyelers.Find(id); // Bu kimliği kullanarak, kullanıcı bilgilerini veritabanından getiriyorum. 
            // Kullanıcıya ait ayarlarını güncellemesi için gerekli olan kısmı hazırlıyorum
            // ve bu bilgilerle birlikte AyarlarPartial görünümünü döndürüyorum.
            return PartialView("AyarlarPartial", uyegetir);
        }


    }
}