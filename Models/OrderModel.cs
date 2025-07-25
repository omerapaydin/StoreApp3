using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreApp3.Models
{
    public class OrderModel
    {
         public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        [BindNever]
        public Cart? Cart { get; set; } 
         public string? CartName { get; set; }
        public string? CartNumber { get; set; }
        public string? ExpirationMonth { get; set; }
        public string? ExpirationYear { get; set; }
        public string? Cvc { get; set; }
    }
}