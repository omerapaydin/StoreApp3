using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Data.Abstract;
using StoreApp3.Entity;

namespace StoreApp3.Data.Concrete.EfCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private IdentityContext _context;
        public EfCategoryRepository(IdentityContext context)
        {
            _context = context;
        }
        public IQueryable<Category> Categories => _context.Categories;

        public void AddCategory(Category category)
        {
           _context.Categories.Add(category);
           _context.SaveChanges();
        }
    }
}