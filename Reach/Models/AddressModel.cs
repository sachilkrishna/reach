using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
    }
}