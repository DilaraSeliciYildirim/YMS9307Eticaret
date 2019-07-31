using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Entity;
using Eticaret.Common;


namespace ETicaret.Repository
{
    public class PaymentRepository
    {
        public static ECommerceEntities db = Tool.GetConnection();
        ResultProcess<Payment> result = new ResultProcess<Payment>();

        public Result<List<Payment>> List()
        {
            return result.GetListResult(db.Payments.ToList());
        }
    }
}
