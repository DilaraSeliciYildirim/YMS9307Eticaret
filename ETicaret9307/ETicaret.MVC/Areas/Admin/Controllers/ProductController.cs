using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Entity;
using ETicaret.Repository;
using ETicaret.MVC.Areas.Admin.Models.ResultModel;
using ETicaret.MVC.Areas.Admin.Models.ProductVM;

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

        //Product ekleme sayfasını açarken, sistemde mevcut olan Category ve Brand'leri getirmeliyiz ki Admin bunlar arasından seçim yapabilsin.

        //CatList ve BrandList'i tek bir modelde toplamak için bir ViewModel class'ı olan ProductViewModel'ı yaratıyoruz.

        [HttpGet]
        public ActionResult AddProduct()
        {
            CategoryRepository cr = new CategoryRepository();
            BrandRepository br = new BrandRepository();
            ProductViewModel pwm = new ProductViewModel();

            pwm.CatList = cr.List().ProcessResult;
            pwm.BrandList = br.List().ProcessResult;

            return View(pwm);
        }

        [HttpPost]
        public ActionResult AddProduct(Product model, HttpPostedFileBase Photo)
        {
            // Brand ve Category kısmı null gelmemeli!!
            string PhotoName = "";
            if (Photo !=null && Photo.ContentLength>0)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                Photo.SaveAs(path);
            }

            model.Photo = PhotoName;
            result.resultint = pr.Insert(model);

            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("ProductList");
            }

            return RedirectToAction("AddProduct");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            //Pwm'yi güncelledik. Çünkü burada listelerin yanısıra Product objesi de göndermemiz gerekmekte.
            CategoryRepository cr = new CategoryRepository();
            BrandRepository br = new BrandRepository();
            ProductViewModel pwm = new ProductViewModel();

            pwm.CatList = cr.List().ProcessResult;
            pwm.BrandList = br.List().ProcessResult;
            pwm.Product = pr.GetObjById(id).ProcessResult;

            return View(pwm);
        }

        public ActionResult EditProduct(Product model, HttpPostedFileBase Photo)
        {
            string Photoname = model.Photo;

            if (Photo !=null && Photo.ContentLength>0)
            {
                Photoname = Guid.NewGuid().ToString().Replace("-", "");

                if (Photo.ContentType=="image/png")
                {
                    Photoname += ".png";
                }
                else if (Photo.ContentType=="image/jpg")
                {
                    Photoname += ".jpg";
                }
                else if (Photo.ContentType=="image/bmp")
                {
                    Photoname += ".bmp";
                }
                else
                {
                    TempData["PhotoExtensionError"] = "Lütfen jpg, png ya da bmp formatında resim yükleyiniz";
                    return RedirectToAction("EditProduct", new { @id = model.ProductId });
                }

                string path = Server.MapPath("~/Images/" + Photoname);
                Photo.SaveAs(path);
            }

            model.Photo = Photoname;
            result.resultint = pr.Update(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("ProductList");
            }
            else
            {
                return RedirectToAction("EditProduct", new { @id = model.ProductId });
            }


        }
    }
}