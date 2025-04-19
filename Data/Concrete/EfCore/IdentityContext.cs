using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreApp.Entity;

namespace StoreApp.Data.Concrete.EfCore
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories  => Set<Category>();
        public DbSet<Comment> Comments => Set<Comment>();
    }
}