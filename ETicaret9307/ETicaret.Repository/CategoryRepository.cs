using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common;
using ETicaret.Entity;

namespace ETicaret.Repository
{
    public class CategoryRepository : DataRepository<Category, Guid>
    {
        static ECommerceEntities db = Tool.GetConnection();
        ResultProcess<Category> result = new ResultProcess<Category>();

        public override Result<int> Delete(Guid id)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryId == id);
            db.Categories.Remove(c);
            return result.GetResult(db);
        }

        public override Result<Category> GetObjById(Guid id)
        {
            Category c = db.Categories.SingleOrDefault(t => t.CategoryId == id);
            return result.GetT(c);
        }

        public override Result<int> Insert(Category item)
        {
            Category yeni = db.Categories.Create();
            yeni.CategoryId = Guid.NewGuid();
            yeni.CategoryName = item.CategoryName;
            yeni.CreatedDate = DateTime.Now;
            yeni.Description = item.Description;

            db.Categories.Add(yeni);
            return result.GetResult(db);
            
        }

        public override Result<List<Category>> List()
        {
            List<Category> CatList = db.Categories.ToList();
            return result.GetListResult(CatList);
        }

        public override Result<int> Update(Category item)
        {
            Category dbdeki = db.Categories.SingleOrDefault(t => t.CategoryId == item.CategoryId);

            dbdeki.CategoryName = item.CategoryName;
            dbdeki.Description = item.Description;

            return result.GetResult(db);
        }
    }
}
