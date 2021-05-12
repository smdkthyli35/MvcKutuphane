using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();

        // GET: Odunc
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(m => m.ISLEMDURUM == false).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> degerler1 = (from i in db.TBLUYELER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD + ' ' + i.SOYAD,
                                                 Value = i.ID.ToString()

                                             }
                                             ).ToList();

            List<SelectListItem> degerler2 = (from i in db.TBLKITAP.Where(m=>m.DURUM==true).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.AD,
                                                  Value = i.ID.ToString()
                                              }
                                              ).ToList();

            List<SelectListItem> degerler3 = (from i in db.TBLPERSONEL.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.PERSONEL,
                                                  Value = i.ID.ToString()
                                              }
                                  ).ToList();

            ViewBag.dgr1 = degerler1;
            ViewBag.dgr2 = degerler2;
            ViewBag.dgr3 = degerler3;
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var d1 = db.TBLUYELER.Where(m => m.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKITAP.Where(m => m.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(m => m.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER = d1;
            p.TBLKITAP = d2;
            p.TBLPERSONEL = d3;
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OduncIade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;

            ViewBag.dgr = d3.TotalDays;

            return View("OduncIade", odn);
        }

        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var odnc = db.TBLHAREKET.Find(p.ID);
            odnc.UYEGETIRTARIH = p.UYEGETIRTARIH;
            odnc.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}