using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StockTrackingMVC.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var sls = db.Sales.ToList();
            var sls = db.Sales.ToList().ToPagedList(sayfa, 10);
            return View(sls);
        }

        [HttpGet]
        public ActionResult AddSales()
        {
            List<SelectListItem> productList = (from p in db.Products.ToList()
                    select new SelectListItem()
                    {
                        Text = p.ProductName,
                        Value = p.ProductID.ToString()
                    }
                ).ToList();
            ViewBag.ProductList = productList;

            List<SelectListItem> employeeList = (from e in db.Employees.ToList()
                    select new SelectListItem()
                    {
                        Text = e.FirstName + " " + e.LastName,
                        Value = e.EmployeeID.ToString()
                    }
                ).ToList();
            ViewBag.EmployeeList = employeeList;

            List<SelectListItem> customerList = (from c in db.Customers.ToList()
                    select new SelectListItem()
                    {
                        Text = c.FirstName + " " + c.LastName,
                        Value = c.CustomerID.ToString()
                    }
                ).ToList();
            ViewBag.CustomerList = customerList;

            var sales = db.Sales.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddSales(Sales sales)
        {
            var product = db.Products.Where(p => p.ProductID == sales.Products.ProductID).SingleOrDefault();
            sales.Products = product;
            decimal unitPrice = Decimal.Parse(product.SalesPrice.ToString());
            decimal total = Decimal.Parse(sales.Amount.ToString()) * unitPrice;
            var employee = db.Employees.Where(e => e.EmployeeID == sales.Employees.EmployeeID).SingleOrDefault();
            sales.Employees = employee;
            var customer = db.Customers.Where(c => c.CustomerID == sales.Customers.CustomerID).SingleOrDefault();
            sales.Customers = customer;
            sales.Total = total;
            sales.DateInfo = DateTime.Now;
            db.Sales.Add(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}