using Newtonsoft.Json;
using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reach.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin, Registered")]
        public ActionResult LandingPage()
        {
            Customer C = Session["ActiveCustomer"] as Customer;
            //HttpCookie cookie1 = HttpContext.Request.Cookies.Get("ActiveCustomer");
            //Customer C = JsonConvert.DeserializeObject<Customer>(cookie1.Value);
            
            //if (C.CustomerType=="Admin")
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminLandingPage");
            }
            else
            {
                return RedirectToAction("CustomerLandingPage");
            }
        }
        [Authorize(Roles = "Registered")]
        public ActionResult CustomerLandingPage()
        {
            Customer C = Session["ActiveCustomer"] as Customer;
            ViewBag.CustomerFName = C.Fname;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminLandingPage()
        {

            return View();
        }
    }
}