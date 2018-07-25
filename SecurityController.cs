using PersonelDeparmanMVCProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelDeparmanMVCProject.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        PersonlDepartmanDBEntities db = new PersonlDepartmanDBEntities();
        // GET: Security
      
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDB = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Soyad == kullanici.Soyad);
            if (kullaniciInDB!=null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDB.Ad,false);
                return RedirectToAction("Index","Departman");

            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}