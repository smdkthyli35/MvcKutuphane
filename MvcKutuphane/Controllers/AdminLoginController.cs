using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        DbKutuphaneEntities db = new DbKutuphaneEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBLADMIN t)
        {
            var bilgiler = db.TBLADMIN.FirstOrDefault(x => x.KULLANICI == t.KULLANICI && x.SIFRE == t.SIFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KULLANICI, false);
                Session["KULLANICI"] = bilgiler.KULLANICI.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return View();
            }
        }
    }
}