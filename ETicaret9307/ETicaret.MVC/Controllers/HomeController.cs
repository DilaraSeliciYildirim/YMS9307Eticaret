using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Repository;

namespace ETicaret.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        ProductRepository pr = new ProductRepository();
        public ActionResult Index()
        {
            return View(pr.GetLatestObjects(5).ProcessResult);
        }
    }
}