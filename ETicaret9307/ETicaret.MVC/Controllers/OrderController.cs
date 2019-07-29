using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Repository;
using ETicaret.Entity;

namespace ETicaret.MVC.Controllers
{
    public class OrderController : Controller
    {
        OrderRepository or = new OrderRepository();
        OrderDetailRepository odr = new OrderDetailRepository();
        ProductRepository pr = new ProductRepository();
        // GET: Order
        public ActionResult AddOrder(int id) // Bu metod ürünlerin altındaki alışveriş arabası butonuyla tetiklenecek.
        {
            //Sepet oluşturacağız, sepeti de session üzerinde tutacağız. Order için yaratacağımız session Session["Order"]
            if (Session["Order"]==null) // sepet oluşturulmamışsa
            {
                Order newOrder = new Order();
                newOrder.IsPay = false;
                or.Insert(newOrder);
                Order dbdeki = or.GetLatestObjects(1).ProcessResult.SingleOrDefault();
                Session["Order"] = dbdeki;

                OrderDetail newOD = new OrderDetail();
                newOD.OrderId = ((Order)Session["Order"]).OrderId;
                newOD.ProductId = id;
                newOD.Quantity = 1;
                newOD.Price = pr.GetObjById(id).ProcessResult.Price;
                odr.Insert(newOD);

            }
            else  //sepet oluşturulmuş ve aynı sepete ürün eklenmek isteniyor.
            {
                Order sepetteki = (Order)Session["Order"];
                OrderDetail sepettekiOrd = odr.GetObjectByTwoId(sepetteki.OrderId, id).ProcessResult;
                // Daha önce sepete eklenmiş bir ürün aynı sepete tekrar eklenmek istenirse, bu ürünün quantity'sini arttırmamız lazım. O sebeple bu üründen sepette mevcut mu diye bakmalıyız.
                if (sepettekiOrd==null) //demek ki sepete yeni bir ürün ekleniyor
                {
                    OrderDetail yeniProductOrd = new OrderDetail();
                    yeniProductOrd.OrderId = sepetteki.OrderId;
                    yeniProductOrd.ProductId = id;
                    yeniProductOrd.Price = pr.GetObjById(id).ProcessResult.Price;
                    yeniProductOrd.Quantity = 1;
                    odr.Insert(yeniProductOrd);
                }
                else
                {
                    sepettekiOrd.Quantity++;
                    sepettekiOrd.Price += pr.GetObjById(id).ProcessResult.Price;
                    odr.Update(sepettekiOrd);

                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}