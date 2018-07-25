using PersonelDeparmanMVCProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelDeparmanMVCProject.ViewModel;
using System.Data.Entity.Validation;

namespace PersonelDeparmanMVCProject.Controllers
{
    [Authorize(Roles = "D")]
    public class PersonelController : Controller
    {
        PersonlDepartmanDBEntities db = new PersonlDepartmanDBEntities();
        // GET: Personel

        [OutputCache(Duration =30)]
        public ActionResult Index()
        {
            
            var model = db.Personel.Include(m=>m.Departman).ToList();

            return View(model);
        }
       
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personeller = new Personel()
                
            };
            return View("PersonelForm",model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(PersonelFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var gnder = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personeller = model.Personeller

                };
                return View("PersonelForm",gnder);
            }
            if ( model.Personeller.Id == 0)
            {
                db.Personel.Add(model.Personeller);
            }

            else
            {
               
                    db.Entry(model.Personeller).State = System.Data.Entity.EntityState.Modified;
           
                
            }

            
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personeller = db.Personel.Find(id)
            };
            return View("PersonelForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();
            }

            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListesi(int id)
        {
            var model = db.Personel.Where(x => x.DepartmanId == id);
            return PartialView(model);
        }
        public int? ToplamMaas()
        {
            return db.Personel.Sum(x=>x.Maas);
        }
    }
}