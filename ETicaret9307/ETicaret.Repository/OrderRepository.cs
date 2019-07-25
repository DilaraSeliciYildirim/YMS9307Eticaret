using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Entity;
using Eticaret.Common;

namespace ETicaret.Repository
{
    public class OrderRepository : DataRepository<Order, int>,GetLatestObject<Order>
    {
        public static ECommerceEntities db = Tool.GetConnection();
        ResultProcess<Order> result = new ResultProcess<Order>();
        public override Result<int> Delete(int id)
        {
            Order deleted = db.Orders.SingleOrDefault(t => t.OrderId == id);
            db.Orders.Remove(deleted);
            return result.GetResult(db);
        }

        public Result<List<Order>> GetLatestObjects(int Quantity)
        {
            return result.GetListResult(db.Orders.OrderByDescending(t => t.OrderId).Take(Quantity).ToList());
        }

        public override Result<Order> GetObjById(int id)
        {
            Order obj = db.Orders.Find(id);
            return result.GetT(obj);
        }

        public override Result<int> Insert(Order item)
        {
            Order newOrder = db.Orders.Create();

            newOrder.IsPay = item.IsPay;
            newOrder.MemberId = item.MemberId;
            newOrder.OrderDate = DateTime.Now;
            newOrder.TotalPrice = item.TotalPrice;


            db.Orders.Add(newOrder);
            return result.GetResult(db);
        }

        public override Result<List<Order>> List()
        {
            return result.GetListResult(db.Orders.ToList());
        }

        public override Result<int> Update(Order item)
        {
            Order updated = db.Orders.Find(item.OrderId);
            updated.IsPay = item.IsPay;
            updated.TotalPrice = item.TotalPrice;

            return result.GetResult(db);
        }
    }
}
