using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Istatistik
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            var deger2 = db.TBLKITAP.Count();
            var deger3 = db.TBLKITAP.Where(m => m.DURUM == false).Count();
            var deger4 = db.TBLCEZALAR.Sum(m => m.PARA);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View();
        }

        public ActionResult Hava()
        {
            return View();
        }

        public ActionResult HavaKart()
        {
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
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

        public ActionResult LinqKart()
        {
            var deger1 = db.TBLKITAP.Count();
            var deger2 = db.TBLUYELER.Count();
            var deger3 = db.TBLCEZALAR.Sum(m => m.PARA);
            var deger4 = db.TBLKITAP.Where(m => m.DURUM == false).Count();
            var deger5 = db.TBLKATEGORI.Count();
            var deger8 = db.EnFazlaKitapYazar().FirstOrDefault();


            var deger11 = db.TBLILETISIM.Count();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr11 = deger11;
            return View();
        }
    }
}