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
    public class CategoryController : Controller
    {
        CategoryRepository cr = new CategoryRepository();
        //Result<int> resultint = new Result<int>();
        //Result<Category> resultT = new Result<Category>();
        //Result<List<Category>> resultList = new Result<List<Category>>();

        //Yukardaki 3 instance'ı kullanmak yerine bunları InstanceResult clasının propertyleri haline getirdik.
        InstanceResult<Category> result = new InstanceResult<Category>();

        // GET: Admin/Category
        public ActionResult List(string mesaj)
        {
            result.resultList = cr.List();
            ViewBag.silme = mesaj;
            return View(result.resultList.ProcessResult);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            result.resultint = cr.Insert(model);
            ViewBag.basarili = result.resultint.UserMessage;
            return View();
        }

        [HttpGet]
        public ActionResult EditCategory(Guid id)
        {
            result.resultT = cr.GetObjById(id);
            return View(result.resultT.ProcessResult);
        }

        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            result.resultint = cr.Update(model);
            ViewBag.mesaj = result.resultint.UserMessage;
            return View(model);
        }

        public ActionResult DeleteCategory(Guid id)
        {
            result.resultint = cr.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMessage });
        }
    }
}