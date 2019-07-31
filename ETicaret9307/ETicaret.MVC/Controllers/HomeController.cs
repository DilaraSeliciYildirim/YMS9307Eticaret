using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Repository;
using ETicaret.Entity;

namespace ETicaret.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        ProductRepository pr = new ProductRepository();
        public ActionResult Index(string paymentMesaj) // Ana Sayfa
        {
            ViewBag.payment = paymentMesaj;
            return View(pr.GetLatestObjects(5).ProcessResult);
        }

        // Soldaki kategori menüsündeki kategori isimlerine tıkladığımızda o kategorideki ürünleri getiren metod.
        public ActionResult GetProductByCatId(Guid id)
        {
            List<Product> ProList = ProductRepository.db.Products.Where(t => t.CategoryId == id).ToList();

            // var PList = pr.List().ProcessResult.Where(t => t.CategoryId == id).ToList();

            return View(ProList);
        }
        // Sağdaki brand menüsündeki brand isimlerine tıkladığımızda o brand'e ait olan ürünleri getiren metod.
        public ActionResult GetProductByBrandId(int id)
        {
            List<Product> Prolist = ProductRepository.db.Products.Where(t => t.BrandId == id).ToList();

            return View(Prolist);
        }

        public ActionResult ListAllProduct()
        {
            return View(pr.List().ProcessResult);
        }

        public ActionResult Detail(int id)
        {
            return View(pr.GetObjById(id).ProcessResult);
        }
    }
}