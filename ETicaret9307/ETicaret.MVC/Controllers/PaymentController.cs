using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Repository;
using ETicaret.Entity;

namespace ETicaret.MVC.Controllers
{
    public class PaymentController : Controller
    {
        PaymentRepository payRep = new PaymentRepository();
        // GET: Payment

        [HttpGet]
        public ActionResult Pay()
        {
            List<Payment> PaymenTypeList = payRep.List().ProcessResult;
            return View(PaymenTypeList);
        }

        [HttpPost]
        public ActionResult Pay(Invoice model)
        {
            var state =ModelState.IsValid;


            if (model.PaymentTypeId==null) //Payment type seçilmemiş
            {
                TempData["NoPaymentId"] = "Lütfen ödeme yöntemi seçiniz";
                return RedirectToAction("Pay");
            }

            Order sepetim = (Order)Session["Order"];
            model.OrderId = sepetim.OrderId;

            InvoiceRepository ip = new InvoiceRepository();
            if (ip.Insert(model).IsSucceeded)
            {
                OrderRepository or = new OrderRepository();
                sepetim.IsPay = true;
                or.Update(sepetim);
                Session.Remove("Order");

                return RedirectToAction("Index", "Home", new { @paymentMesaj = "Ödeme başarılı!" });
               
            }
            else
            {
                TempData["PaymentError"] = "Bir şeyler ters gitti";
                return RedirectToAction("Pay");
            }

           
        }
    }
}