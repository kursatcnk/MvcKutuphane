using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;
namespace MvcKutuphane.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisualizeKitapResult() 
        {
            return Json(liste());
        }
        public List<Grafik> liste()
        {
            List<Grafik> cs=new List<Grafik>();
            cs.Add(new Grafik()
            {
                YayinEvi = "Günes",
                Sayi = 7
            });
            cs.Add(new Grafik()
            {
                YayinEvi = "Yildiz",
                Sayi = 4
            });
            cs.Add(new Grafik()
            {
                YayinEvi = "Test",
                Sayi = 5
            });
            return cs;
        }
    }
}