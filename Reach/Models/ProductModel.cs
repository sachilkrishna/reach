using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class ProductModel
    {
        
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        public string Images { get; set; }
        public int RemainingQuantity { get; set; }
        private int _requiredQuantity = 1;
        public int RequiredQuantity { get { return _requiredQuantity; } set { _requiredQuantity = value; } }
        public double SubTotal { get; set; }


    }
}