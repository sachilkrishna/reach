using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reach.Models
{
    public class LoginModel
    {
        public string UserEmailID { get; set; }
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }

    }
}