using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;

namespace StockTrackingMVC.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index()
        {
            var sls = db.Sales.ToList();
            return View(sls);
        }
    }
}