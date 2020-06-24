using Newtonsoft.Json;
using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Reach.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Guest(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Guest(Customer C, string btnLogin,string ReturnUrl)
        {
            LoginModelManager lmM = new LoginModelManager();
            ViewBag.ReturnUrl = ReturnUrl;
            if (btnLogin== "Login")
            {
                C.Customer_ID = lmM.VerifyLogin(C.LoginPassword, C.LoginEmailID);
                if (C.Customer_ID != 0)
                {
                    C = lmM.GetCustomerById(C);
                }
                else
                {
                    ViewBag.alert = "Username or Password Incorrect";
                    return View("Guest");
                }
            }
            else if (btnLogin == "Sign Up")
            {
                if (!lmM.VerifyRegisteredEmail(C.Email))
                {
                    C.CustomerType = "Registered";
                    C = lmM.RegisterCustomer(C);
                }
                else
                {
                    ViewBag.alertEmail = "Email already taken. Use another";
                    return View("Guest");
                }
            }
           else 
            {
                C.CustomerType = "Guest";
                C = lmM.RegisterCustomer(C);
            }
            Session["ActiveCustomer"] = C;
            string ac = Newtonsoft.Json.JsonConvert.SerializeObject(C);
            HttpCookie cookie1 = new HttpCookie("ActiveCustomer",ac);
            HttpContext.Response.SetCookie(cookie1);
            //HttpContext.Response.Cookies.Remove("ActiveCustomer");

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, C.Email, DateTime.Now, DateTime.Now.AddMinutes(30), C.RememberMe, C.CustomerType, FormsAuthentication.FormsCookiePath);
            string Hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Convert.ToString(Hash));
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);
            return Redirect(ReturnUrl);
        }

        //[HttpPost]
        //public ActionResult Guest(Customer C, string btnLogin, string ReturnUrl)
        //{
        //    LoginModelManager lmM = new LoginModelManager();
        //    ViewBag.ReturnUrl = ReturnUrl;
        //    if (btnLogin == "Login")
        //    {
        //        C.Customer_ID = lmM.VerifyLogin(C.LoginPassword, C.LoginEmailID);
        //        if (C.Customer_ID != 0)
        //        {
        //            C = lmM.GetCustomerById(C);
        //        }
        //        else
        //        {
        //            ViewBag.alert = "Username or Password Incorrect";
        //            return View("Guest");
        //        }
        //    }
        //    else if (btnLogin == "Sign Up")
        //    {
        //        if (!lmM.VerifyRegisteredEmail(C.Email))
        //        {
        //            C.CustomerType = "Registered";
        //            C = lmM.RegisterCustomer(C);
        //        }
        //        else
        //        {
        //            ViewBag.alertEmail = "Email already taken. Use another";
        //            return View("Guest");
        //        }
        //    }
        //    else
        //    {
        //        C.CustomerType = "Guest";
        //        C = lmM.RegisterCustomer(C);
        //    }
        //    Session["ActiveCustomer"] = C;
        //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, C.Email, DateTime.Now, DateTime.Now.AddMinutes(30), C.RememberMe, C.CustomerType, FormsAuthentication.FormsCookiePath);
        //    string Hash = FormsAuthentication.Encrypt(ticket);
        //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Convert.ToString(Hash));
        //    if (ticket.IsPersistent)
        //    {
        //        cookie.Expires = ticket.Expiration;
        //    }
        //    Response.Cookies.Add(cookie);
        //    return Redirect(ReturnUrl);
        //}
        public ActionResult Dummy()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage","Search",new { });
        }


    }
}