using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {
        DbMvcKutuphaneEntities db=new DbMvcKutuphaneEntities();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TblUyeler p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            db.TblUyelers.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}