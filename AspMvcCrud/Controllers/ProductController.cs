using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspMvcCrud.Models;
using PagedList;

namespace AspMvcCrud.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? page, int? pageSize)

        {
            productcatEntities db = new productcatEntities();

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


            //Ddefault size is 5 otherwise take pageSize value
            int defaSize = (pageSize ?? 5);

            ViewBag.psize = defaSize;

            //Dropdownlist code for PageSize selection
            //In View Attach this
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };
            //Create a instance of our DataContext  
            product _dbContext = new product();
            IPagedList<product> productlst = null;

            productlst = db.products.OrderBy(p => p.categoryid).ToPagedList(pageIndex, defaSize);
            return View(productlst);



        }
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Add(product c)
        {
            productcatEntities db = new productcatEntities();
            db.products.Add(c);
            db.SaveChanges();
            db.Dispose();

            return Redirect("Index");
        }
        public ActionResult Edit(int id)

        {
            productcatEntities db = new productcatEntities();
            product e = db.products.Where(i => i.id == id).FirstOrDefault();
            db.Dispose();
            return View(e);

        }
        public ActionResult Save(product p)

        {
            productcatEntities db = new productcatEntities();
            product e = db.products.Where(i => i.id == p.id).FirstOrDefault();
            e.productname = p.productname;
            e.categoryid = p.categoryid;
            db.SaveChanges();
         
            db.Dispose();
            return Redirect("Index");

        }
        public ActionResult Details(int id)

        {
            productcatEntities db = new productcatEntities();
            product e = db.products.Where(i => i.id == id).FirstOrDefault();
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
            product e = db.products.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);


        }
        public ActionResult Remove(int id)

        {
            productcatEntities db = new productcatEntities();
            product e = db.products.Find(id);
            db.products.Remove(e);
            db.SaveChanges();


            return Redirect("Index");


        }

    }
}