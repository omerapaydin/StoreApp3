using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Entity;

namespace StoreApp.Data.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products {get;}
        void AddProduct(Product product);

    }
}