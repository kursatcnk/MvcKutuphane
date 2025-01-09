using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Siniflarim;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        DbMvcKutuphaneEntities db=new DbMvcKutuphaneEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1=db.TblKitaps.ToList();
            cs.Deger2=db.TblHakkimizdas.ToList();        
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TblIletisim t)
        {
            db.TblIletisims.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}