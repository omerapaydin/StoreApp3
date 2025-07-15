using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp3.ViewModel
{
    public class AddProductViewModel
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public decimal? Price { get; set; }
        public string? UserId { get; set; }
        public int? CategoryId { get; set; } 
    }
}