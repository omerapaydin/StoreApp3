using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Entity;

namespace StoreApp3.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

         public void AddItem(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (item == null)
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else 
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
           Items.RemoveAll(i=>i.Product.ProductId == product.ProductId);
        }

        public decimal CalculateTotal()
        {
            return Items.Sum(i => (i.Product.Price ?? 0) * i.Quantity);
        }

        public void Clear()
        {
            Items.Clear();
        }



    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }

    }
}