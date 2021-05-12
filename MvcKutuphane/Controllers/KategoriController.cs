using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Kategori
        public ActionResult Index()
        {
            //var kategoriler = db.TBLKATEGORI.ToList();
            var kategoriler = db.TBLKATEGORI.Where(m => m.DURUM == true).ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            kategori.DURUM = false;
            //db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir", kategori);
        }

        public ActionResult KategoriGuncelle(TBLKATEGORI p)
        {
            var ktg = db.TBLKATEGORI.Find(p.ID);
            ktg.AD = p.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}