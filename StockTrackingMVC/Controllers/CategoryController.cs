using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;

namespace StockTrackingMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Categories category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var category = db.Categories.Find(id);
            return View("UpdateCategory", category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Categories category)
        {
            var cat = db.Categories.Find(category.CategoryID);
            cat.CategoryName = category.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}