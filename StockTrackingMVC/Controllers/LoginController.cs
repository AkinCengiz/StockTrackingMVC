using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTrackingMVC.Models.Entity;
using System.Web.Security;

namespace StockTrackingMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbMVCStockTakipEntities db = new DbMVCStockTakipEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users usr)
        {
            var userInfo = db.Users.SingleOrDefault(u => u.UserName == usr.UserName && u.Password == usr.Password);
            if (userInfo != null)
            {
                FormsAuthentication.SetAuthCookie(userInfo.UserName,false);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }
        }
    }
}