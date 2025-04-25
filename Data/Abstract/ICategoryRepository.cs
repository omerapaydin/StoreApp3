using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Entity;

namespace StoreApp.Data.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories {get;}
        void AddCategory(Category category);
    }
}