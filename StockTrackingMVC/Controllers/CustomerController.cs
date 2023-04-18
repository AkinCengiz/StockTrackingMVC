using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;

namespace StockTrackingMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index()
        {
            var customers = db.Customers.ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customers customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            return View("UpdateCustomer",customer);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customers customer)
        {
            var cstmr = db.Customers.Find(customer.CustomerID);
            cstmr.FirstName = customer.FirstName;
            cstmr.LastName = customer.LastName;
            cstmr.City = customer.City;
            cstmr.Balance = customer.Balance;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}