using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common;
using ETicaret.Entity;

namespace ETicaret.Repository
{
    public class ProductRepository : DataRepository<Product, int>,GetLatestObject<Product>
    {
        public static ECommerceEntities db = new ECommerceEntities();
        ResultProcess<Product> result = new ResultProcess<Product>();
        public override Result<int> Delete(int id)
        {
            Product silinecek = db.Products.SingleOrDefault(t => t.ProductId == id);
            db.Products.Remove(silinecek);
            return result.GetResult(db);
        }

        public Result<List<Product>> GetLatestObjects(int Quantity)
        {
            return result.GetListResult(db.Products.OrderByDescending(t => t.ProductId).Take(Quantity).ToList());

            //Product'ları ProductId'si en büyük olandan küçüğe doğru sıralattık. En tepede son eklenen ürünler oldu. Bu ürünlerden Quantity kadarını listeye attık.
        }

        public override Result<Product> GetObjById(int id)
        {
            return result.GetT(db.Products.SingleOrDefault(t => t.ProductId == id));
        }

        public override Result<int> Insert(Product item)
        {
            Product yeni = db.Products.Create();

            yeni.ProductName = item.ProductName;
            yeni.Stock = item.Stock;
            yeni.Price = item.Price;
            yeni.Photo = item.Photo;
            yeni.CategoryId = item.CategoryId;
            yeni.BrandId = item.BrandId;

            db.Products.Add(yeni);
            return result.GetResult(db);
        }

        public override Result<List<Product>> List()
        {
            return result.GetListResult(db.Products.ToList());
        }

        public override Result<int> Update(Product item)
        {
            Product guncellenecek = db.Products.SingleOrDefault(t => t.ProductId == item.ProductId);

            guncellenecek.BrandId = item.BrandId;
            guncellenecek.CategoryId = item.CategoryId;
            guncellenecek.ProductName = item.ProductName;
            guncellenecek.Price = item.Price;
            guncellenecek.Stock = item.Stock;
            guncellenecek.Photo = item.Photo;

            return result.GetResult(db);
        }
    }
}
