using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp3.Entity
{
    public class Order
    {
      
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int PostId { get; set; }
        public Product product { get; set; } = null!;
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}