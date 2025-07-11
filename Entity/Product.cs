using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp3.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public decimal? Price { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? CategoryId { get; set; } 
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}