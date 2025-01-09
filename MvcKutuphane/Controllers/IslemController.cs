using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class IslemController : Controller
    {
       DbMvcKutuphaneEntities db=new DbMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblHarekets.Where(x => x.islemdurum == true).ToList();
            return View(degerler);
        }
    }
}