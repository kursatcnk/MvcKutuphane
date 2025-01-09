using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        DbMvcKutuphaneEntities db=new DbMvcKutuphaneEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TblAdmin p)
        {
            var login = db.TblAdmins.FirstOrDefault(x => x.kullaniciadi == p.kullaniciadi && x.sifre == p.sifre);
            if (login !=null)
            {
                FormsAuthentication.SetAuthCookie(login.kullaniciadi, false);
                Session["Kullanici"]=login.kullaniciadi.ToString();
                return RedirectToAction("Index", "Kitap");
            }
            else
            {
                return View();
            }
           
        }
    }
}