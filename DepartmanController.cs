using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelDeparmanMVCProject.Models.EntityFramework;

namespace PersonelDeparmanMVCProject.Controllers
{
   
    public class DepartmanController : Controller
    {

        PersonlDepartmanDBEntities db = new PersonlDepartmanDBEntities();


        // GET: Departman
        [HandleError]
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            int a = 10, b = 0;
            int c = a / b;
            return View(model);
        }

        [HttpGet]
        public ActionResult Kaydet()
        {
            
            return View("DepartmanForm",new Departman());
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            if (departman.Id==0)
            {

                db.Departman.Add(departman);
            }
            else
            {

                var guncellencekVeriler = db.Departman.Find(departman.Id);
                if (guncellencekVeriler==null)
                {
                    return HttpNotFound();
                }
                guncellencekVeriler.Ad = departman.Ad;


            }
           
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);

            if (model==null)
            {
                return HttpNotFound();
            }

            return View("DepartmanForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman == null)
            {
                return HttpNotFound();

            }
            db.Departman.Remove(silinecekDepartman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}