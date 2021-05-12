using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Kitap
        public ActionResult Index(string p)
        {
            var kitaplar = from i in db.TBLKITAP select i;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p));
            }
            //var kitaplar = db.TBLKITAP.ToList();
            return View(kitaplar.ToList());
        }

        [HttpGet]
        public ActionResult YeniKitap()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD,
                                                 Value = i.ID.ToString()
                                             }
                                             ).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                       select new SelectListItem
                                       {
                                           Text = i.AD + ' ' + i.SOYAD,
                                           Value = i.ID.ToString()
                                       }
                                       ).ToList();
            ViewBag.dgr2 = deger2;

            return View();
        }

        [HttpPost]
        public ActionResult YeniKitap(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORI.Where(m => m.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(m => m.ID == p.TBLYAZAR.ID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapGetir(int id)
        {
            var kitap = db.TBLKITAP.Find(id);

            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }
                                           ).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }
                                       ).ToList();
            ViewBag.dgr2 = deger2;

            return View("KitapGetir", kitap);
        }

        public ActionResult KitapGuncelle(TBLKITAP p)
        {
            var kitap = db.TBLKITAP.Find(p.ID);
            kitap.AD = p.AD;
            kitap.BASIMYIL = p.BASIMYIL;
            kitap.YAYINEVI = p.YAYINEVI;
            kitap.SAYFA = p.SAYFA;
            kitap.DURUM = true;
            var ktg = db.TBLKATEGORI.Where(m => m.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(m => m.ID == p.TBLYAZAR.ID).FirstOrDefault();
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}