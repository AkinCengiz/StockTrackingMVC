using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;

namespace StockTrackingMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employees employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employees employee)
        {
            var emp = db.Employees.Find(employee.EmployeeID);
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.Department = employee.Department;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}