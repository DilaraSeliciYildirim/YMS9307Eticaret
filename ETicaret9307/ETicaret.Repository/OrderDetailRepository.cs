using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Entity;
using Eticaret.Common;

namespace ETicaret.Repository
{
    public class OrderDetailRepository : DataRepository<OrderDetail, int>, DeleteObjectByTwoId<int>, GetObjectByTwoId<OrderDetail> //OrderId'yi
    {
        public static ECommerceEntities db = Tool.GetConnection();
        ResultProcess<OrderDetail> result = new ResultProcess<OrderDetail>();
        public override Result<int> Delete(int id) // OrderId'ye siler. 
        {
            //Aynı OrderId ile birden fazla OrderDetail olabilir. O sebeple Liste tipiyle data çektik.
            List<OrderDetail> silinecekler = db.OrderDetails.Where(t => t.OrderId == id).ToList();
            db.OrderDetails.RemoveRange(silinecekler);
            return result.GetResult(db);
        }

        public Result<int> DeleteObjects(int id1, int id2)// id1=OrderId, id2=ProductId
        {
            OrderDetail ord = db.OrderDetails.SingleOrDefault(t => t.OrderId == id1 && t.ProductId == id2);
            db.OrderDetails.Remove(ord);
            return result.GetResult(db);
        }

        public override Result<OrderDetail> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public Result<OrderDetail> GetObjectByTwoId(int Id1, int Id2) //Id1=OrderId, Id2=ProductId
        {
            OrderDetail od = db.OrderDetails.SingleOrDefault(t => t.OrderId == Id1 && t.ProductId == Id2);

            return result.GetT(od);
        }

        public override Result<int> Insert(OrderDetail item)
        {
            OrderDetail newOD = db.OrderDetails.Create();
            newOD.ProductId = item.ProductId;
            newOD.OrderId = item.OrderId;
            newOD.Price = item.Price;
            newOD.Quantity = item.Quantity;

            db.OrderDetails.Add(newOD);
            return result.GetResult(db);
            
        }

        public override Result<List<OrderDetail>> List()
        {
            return result.GetListResult(db.OrderDetails.ToList());
        }

        public override Result<int> Update(OrderDetail item)
        {
            OrderDetail od = GetObjectByTwoId(item.OrderId, item.ProductId).ProcessResult;

            od.Price = item.Price;
            od.Quantity = item.Quantity;

            return result.GetResult(db);
        }
    }
}
