using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common;

namespace ETicaret.Repository
{
    public interface DeleteObjectByTwoId<T>
    {
        Result<T> DeleteObjects(int id1, int id2); // id1=OrdId, id2=ProId
    }
}
