using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reach.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Registered, Guest")]
        public ActionResult PlaceOrder()
        {
            Customer c = Session["ActiveCustomer"] as Customer;
            IEnumerable<ProductModel> ie = Session["ie"] as IEnumerable<ProductModel>;
            List<ProductModel> productModels = ie.ToList();
            Order o = new Order();
            {
                o.CustomerId = c.Customer_ID;
                o.PaymentStatus = "Successful";
                o.PaymentMode = "Online";
                o.PaymentReference = "test";
                o.PaymentAmount = productModels[0].SubTotal;
                o.OrderDate = System.DateTime.Now;
                o.ShippedDate = o.OrderDate.AddDays(1);
                o.ExpectedDeliveryDate = o.OrderDate.AddDays(3);
            }
            OrderModelManager omM = new OrderModelManager();
            o.OrderId= omM.AddOrder(o, productModels);
            ViewBag.OrderId = o.OrderId;
            Session["dpm"] = null;
            return View("OrderSuccessful");
        }


        //ADMIN ----------------------------------------------------------------------------------------------
        [Authorize(Roles ="Admin")]
        public  ActionResult AdminOrder()
        {
            return View("AdminOrder");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetOrders()
        {
            OrderModelManager omM = new OrderModelManager();
            List<Order> lstO = omM.GetAllOrders();
            return Json(lstO, JsonRequestBehavior.AllowGet);
        }
        //ADMIN ----------------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]
        public ActionResult AdminAllOrderItem()
        {
            return View("AdminAllOrderItem");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllOrderItems()
        {
            OrderModelManager omM = new OrderModelManager();
            List<OrderItem> lstOI = omM.GetOrderItems();
            return Json(lstOI, JsonRequestBehavior.AllowGet);
        }
        //ADMIN , CUSTOMER----------------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]
        public ActionResult AdminOrderItem(int? OrderId)
        {
            Session["OrderId"] = OrderId;
            return View("AdminOrderItem");
        }

        [Authorize(Roles = "Admin, Registered")]
        public ActionResult GetOrderItems()
        {
            int? OrderId= Session["OrderId"] as  int?;
            OrderModelManager omM = new OrderModelManager();
            List<OrderItem> lstOI = omM.GetOrderItems(OrderId);
            return Json(lstOI, JsonRequestBehavior.AllowGet);
        }
        //ADMIN ,CUSTOMER----------------------------------------------------------------------------------------------
       
        //CUSTOMER ----------------------------------------------------------------------------------------------
        [Authorize(Roles = "Registered")]
        public ActionResult CustomerOrder()
        {
            return View("CustomerOrder");
        }
        [Authorize(Roles = "Registered")]
        public ActionResult GetCustomerOrders()
        {
            Customer c = Session["ActiveCustomer"] as Customer;
            OrderModelManager omM = new OrderModelManager();
            List<Order> lstO = omM.GetOrdersByCid(c);
            return Json(lstO, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Registered")]
        public ActionResult CustomerOrderItem(int? OrderId)
        {
            Session["OrderId"] = OrderId;
            return View("CustomerOrderItem");
        }
        //CUSTOMER ----------------------------------------------------------------------------------------------
    }
}