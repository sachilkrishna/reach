using Newtonsoft.Json;
using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Reach
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ProductModelManager pmM = new ProductModelManager();
            List<string> result = pmM.FetchCategory();
            Application["CategoriesFetched"] = result;
            HttpCookie cookie1 = null;
            Customer C = null;
            //HttpContext hp = new HttpContext();
            //if (HttpContext.Current.Cookies.Get("ActiveCustomer") != null)
            //{
            //     cookie1 = HttpContext.Request.Cookies.Get("ActiveCustomer");
            //     C = JsonConvert.DeserializeObject<Customer>(cookie1.Value);
            //}


        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userdata = ticket.UserData;
                        string[] role = userdata.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, role);
                        //HttpContext.Current.
                       
                    }
                }
            }
        }
    }
}
