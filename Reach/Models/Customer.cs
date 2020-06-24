using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class Customer
    {
        public int Customer_ID { get; set; }
        public string CustomerType { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Email { get; set; }
        public string AltEmail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public int ShippingAddressId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string LoginEmailID { get; set; }
        public string LoginPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}