using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common;

namespace ETicaret.Repository
{
    interface GetObjectByTwoId<T>
    {
        Result<T> GetObjectByTwoId(int Id1, int Id2);
    }
}
