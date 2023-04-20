using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;

namespace StockTrackingMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index(string word)
        {
            var products = db.Products.Where(p => p.IsActive == true).ToList();
            //var products = from product in db.Products select product;
            if (!string.IsNullOrEmpty(word))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(word.ToLower())).ToList();
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> categoryList = (from c in db.Categories.ToList()
                    select new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryID.ToString()
                    }
                ).ToList();
            ViewBag.listOfCategory = categoryList;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Products product)
        {
            var ctg = db.Categories.Where(c => c.CategoryID == product.Categories.CategoryID).SingleOrDefault();
            product.Categories = ctg;
            product.IsActive = true;
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            product.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            List<SelectListItem> ctg = (from c in db.Categories.ToList()
                    select new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryID.ToString()
                    }
                ).ToList();
            ViewBag.listCtg = ctg;

            var product = db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Products product)
        {
            var ctg = db.Categories.Where(c => c.CategoryID == product.Categories.CategoryID).SingleOrDefault();
            var prdct = db.Products.Find(product.ProductID);
            prdct.ProductName = product.ProductName;
            prdct.Brand = product.Brand;
            prdct.StockAmount = product.StockAmount;
            prdct.PurchasePrice = product.PurchasePrice;
            prdct.SalesPrice = product.SalesPrice;
            prdct.Categories = ctg;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}