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
        public ActionResult List()
        {
            result.resultList = cr.List();
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
    }
}