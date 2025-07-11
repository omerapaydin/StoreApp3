using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StoreApp3.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ImageFile { get; set; }
        public List<Comment> Comments => new List<Comment>();
        public List<Product> Products => new List<Product>();
    }
}