using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StoreApp3.Data.Abstract;
using StoreApp3.Helpers;
using StoreApp3.Models;

namespace StoreApp3.pages
{
    public class CartPageModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public CartPageModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Cart Carts { get; set; } = new Cart();

        public void OnGet()
        {
            Carts = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int ProductId)
        {
            var product = _productRepository.Products.FirstOrDefault(i => i.ProductId == ProductId);

            if (product == null)
            {
                return RedirectToPage("/Cart");
            }

            Carts = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Carts.AddItem(product, 1);
            HttpContext.Session.SetJson("cart", Carts);

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostRemove(int ProductId)
        {
            Carts = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            var itemToRemove = Carts.Items.FirstOrDefault(p => p.Product.ProductId == ProductId);

            if (itemToRemove != null)
            {
                Carts.RemoveItem(itemToRemove.Product);
                HttpContext.Session.SetJson("cart", Carts);
            }

            return RedirectToPage("/Cart");
        }
    }
}