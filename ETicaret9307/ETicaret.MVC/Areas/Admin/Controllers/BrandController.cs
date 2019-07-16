using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.MVC.Areas.Admin.Models.ResultModel;
using ETicaret.Entity;
using ETicaret.Repository;

namespace ETicaret.MVC.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        BrandRepository br = new BrandRepository();
        InstanceResult<Brand> result = new InstanceResult<Brand>();
        public ActionResult BrandList()
        {
            result.resultList = br.List();
            return View(result.resultList.ProcessResult);
        }

        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }

        public ActionResult AddBrand(Brand model, HttpPostedFileBase file)
        {
            string PhotoName = ""; //Resmin adı
            if (file !=null && file.ContentLength>0)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName); //Resmin kaydolacağı yer.
                file.SaveAs(path); //Resmi belirtilen path'e kaydettik.
            }

            model.Photo = PhotoName; //model'e path'i değil PhotoName'i veriyoruz li path üzerinde bir değişiklik yapılmak istenirse işlem zorlaşmasın
            result.resultint = br.Insert(model);

            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("BrandList");
            }
            else
            {
                ViewBag.hata = result.resultint.UserMessage;
                return RedirectToAction("AddBrand");
            }
        }
    }
}