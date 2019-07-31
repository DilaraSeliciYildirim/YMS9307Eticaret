using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Entity;
using Eticaret.Common;

namespace ETicaret.Repository
{
    public class InvoiceRepository
    {
        public static ECommerceEntities db = Tool.GetConnection();
        ResultProcess<Invoice> result = new ResultProcess<Invoice>();

        public Result<int> Insert(Invoice item)
        {
            Invoice newInvoice = db.Invoices.Create();

            newInvoice.Address = item.Address;
            newInvoice.OrderId = item.OrderId;
            newInvoice.PaymentDate = DateTime.Now;
            newInvoice.PaymentTypeId = item.PaymentTypeId;

            db.Invoices.Add(newInvoice);

            return result.GetResult(db);

        }

    }
}
