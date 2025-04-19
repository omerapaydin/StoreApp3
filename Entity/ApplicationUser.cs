using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public string? FullName { get; set; }
        public string? ImageFile { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}