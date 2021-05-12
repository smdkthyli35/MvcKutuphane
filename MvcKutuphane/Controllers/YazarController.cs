using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Yazar
        public ActionResult Index()
        {
            var yazarlar = db.TBLYAZAR.ToList();
            return View(yazarlar);
        }

        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR p)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYAZAR.Find(id);
            return View("YazarGetir", yzr);
        }

        public ActionResult YazarGuncelle(TBLYAZAR p)
        {
            var yazar = db.TBLYAZAR.Find(p.ID);
            yazar.AD = p.AD;
            yazar.SOYAD = p.SOYAD;
            yazar.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKITAP.Where(m => m.YAZAR == id).ToList();
            var yzrad = db.TBLYAZAR.Where(m => m.ID == id).Select(m => m.AD + " " + m.SOYAD).FirstOrDefault();
            ViewBag.yzr1 = yzrad;
            return View(yazar);
        }
    }
}