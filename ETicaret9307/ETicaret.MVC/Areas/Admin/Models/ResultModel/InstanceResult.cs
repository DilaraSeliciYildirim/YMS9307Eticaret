using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eticaret.Common;

namespace ETicaret.MVC.Areas.Admin.Models.ResultModel
{
    public class InstanceResult<T>
    {
        public Result<int> resultint { get; set; }
        public Result<T> resultT { get; set; }
        public Result<List<T>> resultList { get; set; }

    }
}