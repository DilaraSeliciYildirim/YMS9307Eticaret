using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Entity;
using ETicaret.MVC.Areas.Admin.Models.ResultModel;
using ETicaret.Repository;

namespace ETicaret.MVC.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        // GET: Admin/Member
        MemberRepository mr = new MemberRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();
        public ActionResult MemberList()
        {
            result.resultList = mr.List();
            return View(result.resultList.ProcessResult);
        }

        [HttpGet]
        public ActionResult AddMember(string mesajUye, string mesajPassword)
        {
            ViewBag.mesajUye = mesajUye;
            ViewBag.mesajPassword = mesajPassword;
            return View();
        }

        [HttpPost]
        public ActionResult AddMember(Member model, string confirm)
        {
            //Bu maille daha önce kaydolmuş biri var mı?
            var zatenUye = MemberRepository.db.Members.SingleOrDefault(t => t.Email == model.Email); //burdan bir kullanıcı dönüyorsa demek ki modelde belirtilen maille daha önce biri kaydolmuş.

            if (zatenUye==null) //modeldeki maille bir üye yok.
            {
                if (model.Password==confirm)
                {
                    model.RoleId = 1; //admin olarak ekliyoruz.
                    result.resultint = mr.Insert(model);
                    if (result.resultint.IsSucceeded)
                    {
                        return RedirectToAction("MemberList");
                    }

                    return RedirectToAction("AddMember");
                    
                }
                else
                {
                    return RedirectToAction("AddMember", new { @mesajPassword = "Parolalar uyuşmuyor" });
                }

            }
            else
            {
                return RedirectToAction("AddMember", new { @mesajUye = "Bu maille kayıtlı bir admin bulunmaktadır" });
            }
        }
    }
}