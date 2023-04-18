using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;

namespace StockTrackingMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
    }
}