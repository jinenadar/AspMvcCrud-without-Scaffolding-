using AspMvcCrud.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AspMvcCrud.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            productcatEntities db = new productcatEntities();
            List<category> ListCategory = db.categories.ToList();
            db.Dispose();
            return View(ListCategory);
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Add(category c)
        {
            productcatEntities db = new productcatEntities();
            db.categories.Add(c);
            db.SaveChanges();
            db.Dispose();

            return Redirect("Index");
        }
        public ActionResult Edit(int id)

        {
            productcatEntities db = new productcatEntities();
            category e = db.categories.Where(i => i.id == id).FirstOrDefault();
            db.Dispose();
            return View(e);

        }

        public ActionResult Save(category s)

        {
            productcatEntities db = new productcatEntities();
            category e = db.categories.Where(i => i.id == s.id).FirstOrDefault();
            e.categoryname = s.categoryname;
            db.SaveChanges();
            db.Dispose();

            return Redirect("Index");
            

        }

        public ActionResult Details(int id)

        {
            productcatEntities db = new productcatEntities();
            category e = db.categories.Where(i => i.id == id).FirstOrDefault();
            
            db.Dispose();

            return View(e);


        }
        public ActionResult Delete(int id)

        {

            productcatEntities db = new productcatEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category e = db.categories.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);


        }
        public ActionResult Remove(int id)

        {
            productcatEntities db = new productcatEntities();
            category e = db.categories.Find(id);
            db.categories.Remove(e);
            db.SaveChanges();
           

            return Redirect("Index");


        }

    }
}