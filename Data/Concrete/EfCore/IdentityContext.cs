using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StoreApp3.Entity;

namespace StoreApp3.Data.Concrete.EfCore
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Comment> Comments => Set<Comment>();
        
          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<ApplicationUser>();

            var user1 = new ApplicationUser
            {
                Id = "1",
                UserName = "omerapaydin",
                Email = "info@gmail.com",
                ImageFile = "p1.jpg",
                FullName = "Ömer Apaydın",
                EmailConfirmed = true
            };
            user1.PasswordHash = hasher.HashPassword(user1, "User123!");

            var user2 = new ApplicationUser
            {
                Id = "2",
                UserName = "ahmettambuga",
                Email = "info2@gmail.com",
                ImageFile = "p2.jpg",
                FullName = "Ahmet Tamboğa",
                EmailConfirmed = true
            };
            user2.PasswordHash = hasher.HashPassword(user2, "User2Password!");

            modelBuilder.Entity<ApplicationUser>().HasData(user1, user2);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Ayakkabı" },
                new Category { CategoryId = 2, Name = "Kyafet" },
                new Category { CategoryId = 3, Name = "Aksesuarlar" }
            );

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                Title = "Nike",
                Description = "Air Force 2",
                PublishedOn = new DateTime(2024, 1, 1),
                Image = "nike1.jpeg",
                Price = 45000,
                IsActive = true,
                UserId = "1",
                CategoryId = 1
            },
            new Product
            {
                ProductId = 2,
                Title = "Nike",
                Description = "Air Zoom 3",
                PublishedOn = new DateTime(2024, 2, 1),
                Image = "nike2.jpeg",
                Price = 55000,
                IsActive = true,
                UserId = "1",
                CategoryId = 1
            },
            new Product
            {
                ProductId = 3,
                Title = "Nike",
                Description = "Pro Max 2",
                PublishedOn = new DateTime(2024, 3, 1),
                Image = "nike3.jpeg",
                Price = 75000,
                IsActive = true,
                UserId = "2",
                CategoryId = 1
            },
            new Product
            {
                ProductId = 4,
                Title = "Nike",
                Description = "Apple AirPods Pro 2",
                PublishedOn = new DateTime(2024, 3, 1),
                Image = "nike4.jpeg",
                Price = 75000,
                IsActive = true,
                UserId = "2",
                CategoryId = 2
            },
            new Product
            {
                ProductId = 5,
                Title = "Nike",
                Description = "Infinity Tour 3",
                PublishedOn = new DateTime(2024, 3, 1),
                Image = "nike5.jpeg",
                Price = 75000,
                IsActive = true,
                UserId = "2",
                CategoryId = 2
            },
            new Product
            {
                ProductId = 6,
                Title = "Nike",
                Description = "Air Jordan 1",
                PublishedOn = new DateTime(2024, 3, 1),
                Image = "nike6.jpeg",
                Price = 75000,
                IsActive = true,
                UserId = "2",
                CategoryId = 3
            }
        );
        }

    }

   
}