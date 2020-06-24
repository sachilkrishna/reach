using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reach.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult HomePage()
        {

            return View();
        }

        public ActionResult SearchProduct(string NavCategory, string NavKey)
        {
            SearchKey sk = new SearchKey();
            sk.Category = NavCategory;
            sk.Key = NavKey;
            ViewBag.Category = HttpContext.Application["CategoriesFetched"] as IEnumerable<SelectListItem>;
            return View(sk);
        }

        public ActionResult DisplayProduct(string Pid)
        {
            ProductModelManager pmM = new ProductModelManager();
            ProductModel pm = pmM.GetProductByProductID(Pid);
            return View(pm);
        }

    }
}