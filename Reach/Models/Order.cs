using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PaymentStatus { get; set; }
        //public int PaymentStatusID { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentReference { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

    }
}