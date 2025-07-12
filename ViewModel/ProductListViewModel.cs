using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Entity;

namespace StoreApp3.ViewModel
{
    public class ProductListViewModel
    {
        public List<Product>? Products { get; set; }
        public List<Category>? Category { get; set; }
    }
}