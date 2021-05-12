using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    [Authorize]
    public class PanelimController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Panelim

       
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            //var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail);
            var degerler = db.TBLDUYURULAR.ToList();
            

            var d1 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.AD).FirstOrDefault();
            ViewBag.d1 = d1;

            var d2 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;

            var d3 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;

            var d4 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.KULLANICIADI).FirstOrDefault();
            ViewBag.d4 = d4;

            var d5 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.OKUL).FirstOrDefault();
            ViewBag.d5 = d5;

            var d6 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.TELEFON).FirstOrDefault();
            ViewBag.d6 = d6;

            var d7 = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;

            var uyeid = db.TBLUYELER.Where(m => m.MAIL == uyemail).Select(x => x.ID).FirstOrDefault();
            var d8 = db.TBLHAREKET.Where(x => x.UYE == uyeid).Count();
            ViewBag.d8 = d8;

            var d9 = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count();
            ViewBag.d9 = d9;

            var d10 = db.TBLDUYURULAR.Count();
            ViewBag.d10 = d10;

            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index5(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBLUYELER.FirstOrDefault(m => m.MAIL == kullanici);
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.OKUL = p.OKUL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(m => m.MAIL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(m => m.UYE == id).ToList();
            return View(degerler);
        }

        public ActionResult Duyurular()
        {
            var duyuruListesi = db.TBLDUYURULAR.ToList();
            return View(duyuruListesi);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }

        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        public PartialViewResult Partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(m => m.MAIL == kullanici).Select(m => m.ID).FirstOrDefault();
            var uyebul = db.TBLUYELER.Find(id);
            return PartialView("Partial2",uyebul);
        }
    }
}