using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reach.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize(Roles = "Registered, Guest")]
        public ActionResult CustomerAddresses()
        {
            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            List<AddressModel> lstC = cmM.GetAvailableAddresses(c.Customer_ID);
            ViewBag.lstC = lstC;
            //return View(c.ShippingAddressId);
            return View();
        }

        [Authorize(Roles = "Registered, Guest")]
        public ActionResult ShowAvailableAddresses()
        {
            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            List<AddressModel>  lstC =cmM.GetAvailableAddresses(c.Customer_ID);
            ViewBag.lstC = lstC;
            //return View(c.ShippingAddressId);
            return View();
        }

        

        [Authorize(Roles = "Registered, Guest")]
        public ActionResult ChangeShippingAddress( int ShippingAddressId)
        {
            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            c.ShippingAddressId = ShippingAddressId;
            cmM.UpdateShippingAddress(c);
            Session["ActiveCustomer"] = c;
            return RedirectToAction("OrderSummary","Cart");
        }

        [HttpGet]
        [Authorize(Roles = "Registered, Guest")]
        public ActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Registered, Guest")]
        public ActionResult AddAddress(AddressModel a)
        {

            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            cmM.AddNewAddress(c.Customer_ID, a);
            return RedirectToAction("ShowAvailableAddresses");
        }
        [Authorize(Roles = "Registered, Guest")]
        public ActionResult RemoveAddress(int Aid)
        {

            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            cmM.RemoveAddressById(Aid);
            return RedirectToAction("CustomerAddresses");
        }
        [Authorize(Roles = "Registered")]
        public void AddToWishList(int Pid)
        {
            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            cmM.AddPidToWishList(c.Customer_ID, Pid);
            //return RedirectToAction("DisplayProduct", "Search",new { Pid } );
            //return RedirectToAction("DisplayCart");
        }   
    }
}