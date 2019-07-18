using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETicaret.Entity;

namespace ETicaret.MVC.Areas.Admin.Models.ProductVM
{
    public class ProductViewModel
    {
        public List<Category> CatList { get; set; }
        public List<Brand> BrandList { get; set; }
        public Product Product { get; set; }
    }
}