using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcKutuphane
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SifreKolonlariniGenislet();
        }

        // Şifre özeti 72 karakter; eski yedekte sifre sütunları varchar(20). Eski veritabanıyla açılırsa sütunlar genişletiliyor.
        // Aynı işi elle yapmak için: Database/sifre-kolonlari.sql
        static void SifreKolonlariniGenislet()
        {
            using (var db = new Models.Entity.DbMvcKutuphaneEntities())
            {
                db.Database.ExecuteSqlCommand(
                    "IF COL_LENGTH('TblUyeler', 'sifre') < 100 ALTER TABLE TblUyeler ALTER COLUMN sifre varchar(100) NULL; " +
                    "IF COL_LENGTH('TblAdmin', 'sifre') < 100 ALTER TABLE TblAdmin ALTER COLUMN sifre varchar(100) NULL;");
            }
        }
    }
}
