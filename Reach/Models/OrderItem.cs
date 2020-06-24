using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double ListPrice { get; set; }
        public double Discount { get; set; }
        public string OrderStatus { get; set; }
        public int OrderStatusId { get; set; }

    }
}