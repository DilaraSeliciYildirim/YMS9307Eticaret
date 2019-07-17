using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Entity;
using ETicaret.Repository;
using ETicaret.MVC.Areas.Admin.Models.ResultModel;

namespace ETicaret.MVC.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product

        ProductRepository pr = new ProductRepository();
        InstanceResult<Product> result = new InstanceResult<Product>();
        public ActionResult ProductList()
        {
            result.resultList = pr.List();
            return View(result.resultList.ProcessResult);
        }
    }
}