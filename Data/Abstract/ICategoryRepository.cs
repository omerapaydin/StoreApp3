using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Entity;

namespace StoreApp3.Data.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories {get;}
        void AddCategory(Category category);
    }
}