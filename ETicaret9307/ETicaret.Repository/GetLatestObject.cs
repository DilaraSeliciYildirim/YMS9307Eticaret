using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common;

namespace ETicaret.Repository
{
    public interface GetLatestObject<T>
    {
        Result<List<T>> GetLatestObjects(int Quantity);
    }
}
