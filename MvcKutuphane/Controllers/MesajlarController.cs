using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Mesajlar
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(m => m.ALICI == uyemail.ToString()).ToList();
            return View(mesajlar);
        }

        public ActionResult GidenMesaj()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(m => m.GONDEREN == uyemail.ToString()).ToList();
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR p)
        {
            var uyemail = (string)Session["Mail"].ToString();
            p.GONDEREN = uyemail.ToString();
            p.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("GidenMesaj", "Mesajlar");
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var gelenSayisi = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count();
            ViewBag.d1 = gelenSayisi;

            var gidenSayisi = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.d2 = gidenSayisi;
            return PartialView();
        }
    }
}