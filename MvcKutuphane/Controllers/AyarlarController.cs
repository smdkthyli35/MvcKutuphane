using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: Ayarlar
        public ActionResult Index()
        {
            var kullanicilar = db.TBLADMIN.ToList();
            return View(kullanicilar);
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniAdmin(TBLADMIN p)
        {
            db.TBLADMIN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AdminSil(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            db.TBLADMIN.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            return View("AdminGuncelle",admin);
        }

        [HttpPost]
        public ActionResult AdminGuncelle(TBLADMIN t)
        {
            var admin = db.TBLADMIN.Find(t.ID);
            admin.KULLANICI = t.KULLANICI;
            admin.SIFRE = t.SIFRE;
            admin.YETKI = t.YETKI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminLogin");
        }
    }
}