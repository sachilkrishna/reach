using Reach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reach.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ShowProduct()
        {
            return View();
        }
        public ActionResult GetProduct(SearchKey sk,string Category, string Key)
        {
            ProductModelManager pmM = new ProductModelManager();
            List<ProductModel> lstPm = new List<ProductModel>();
            if (!String.IsNullOrWhiteSpace(Key))
            {
                lstPm = pmM.GetProductByKeyword(Key, Category);
            }
            return Json(lstPm,JsonRequestBehavior.AllowGet);
        }
        public ActionResult FetchCategories()
        {
            List<string> result =(List<string>)HttpContext.Application["CategoriesFetched"];
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}