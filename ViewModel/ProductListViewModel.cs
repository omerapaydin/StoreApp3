using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Entity;

namespace StoreApp.ViewModel
{
    public class ProductListViewModel
    {
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
    }
}