using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Data.Abstract;
using StoreApp3.Entity;

namespace StoreApp3.Data.Concrete.EfCore
{
    public class EfProductRepository : IProductRepository
    {
        private readonly IdentityContext _context;
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