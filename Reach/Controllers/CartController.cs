using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reach.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult DisplayCart()
        {
            List<ProductModel> lstpm = new List<ProductModel>();
            ProductModelManager ppM = new ProductModelManager();
            Dictionary<string, int> dpm = null;
            double Total = 0;

            if (Session["dpm"] != null)
            {
                dpm = Session["dpm"] as Dictionary<string, int>;
                foreach (KeyValuePair<string, int> item in dpm)
                {
                    ProductModel pm = ppM.GetProductByProductID(item.Key);
                    pm.RequiredQuantity = Convert.ToInt32(item.Value);
                    pm.Amount = (pm.Rate)*Convert.ToDouble(pm.RequiredQuantity);
                    Total = Total + pm.Amount;
                    lstpm.Add(pm);
                }
                if (lstpm.Count>0)
                {
                    lstpm[0].SubTotal = Total;
                }


                IEnumerable<ProductModel> ie = lstpm as IEnumerable<ProductModel>;
                Session["ie"] = ie;
                return View(ie);
            }
            else
            {
                dpm = new Dictionary<string, int>();
                return View("DisplayEmptyCart");
            }
        }

        public ActionResult AddToCart(string Pid, string RequiredQuantity)
        {
            Dictionary<string, int> dpm = null;
            if (Session["dpm"] != null)
            {
               dpm = Session["dpm"] as Dictionary<string, int>;
            }
            else
            {
               dpm = new Dictionary<string, int>();
            }

            if (dpm.ContainsKey(Pid))
            {
                dpm[Pid]= dpm[Pid] + Convert.ToInt32(RequiredQuantity);
            }
            else
            {
                dpm.Add(Pid, Convert.ToInt32(RequiredQuantity));
            }
            Session["dpm"] = dpm;
            return RedirectToAction("DisplayCart");
        }
        public ActionResult RemoveFromCart(string Pid)
        {
            Dictionary<string, int> dpm = Session["dpm"] as Dictionary<string, int>;
            dpm.Remove(Pid);
            if (dpm.Count==0)
            {
                Session["dpm"] = null;
            }
            return RedirectToAction("DisplayCart");
        }
        public ActionResult EditCart(string Pid, string RequiredQuantity)
        {
            Dictionary<string, int> dpm = Session["dpm"] as Dictionary<string, int>;
            dpm[Pid]=Convert.ToInt32(RequiredQuantity);
            Session["dpm"] = dpm;
            return RedirectToAction("DisplayCart");
        }

        [Authorize(Roles = "Registered, Guest")]
        public ActionResult OrderSummary()
        {
            IEnumerable<ProductModel> ie = Session["ie"] as IEnumerable<ProductModel>;
            Customer c = Session["ActiveCustomer"] as Customer;
            CustomerModelManager cmM = new CustomerModelManager();
            AddressModel a = cmM.FetchAddressById(c.ShippingAddressId);
            return View(a);
        }

        //[Authorize(Roles = "Guest")]
        [Authorize(Roles = "Registered, Guest")]
        public ActionResult CheckOut()
        {
            //if (Session["ActiveCustomer"] != null)
            //{
            //    Customer c = Session["ActiveCustomer"] as Customer;
            //    return RedirectToAction("OrderSummary", "Cart", new { });
            //}
            //return RedirectToAction("Guest","Login",new { });
            return RedirectToAction("OrderSummary", "Cart", new { });
        }


     
    }
}