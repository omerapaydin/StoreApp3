using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp3.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<Product> Products => new List<Product>();
    }
}