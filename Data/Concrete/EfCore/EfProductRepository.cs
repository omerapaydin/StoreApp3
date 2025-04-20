using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Data.Abstract;
using StoreApp.Entity;

namespace StoreApp.Data.Concrete.EfCore
{
    public class EfProductRepository : IProductRepository
    {
        private IdentityContext _context;
        public EfProductRepository(IdentityContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;

        public void AddProduct(Product product)
        {
           _context.Products.Add(product);
           _context.SaveChanges();
        }
    }
}