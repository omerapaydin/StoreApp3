using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp3.Data.Abstract;
using StoreApp3.ViewModel;

namespace StoreApp3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int? id,string? q)
        {
            var products = await _productRepository.Products.ToListAsync();

            if (!string.IsNullOrEmpty(q))
            {
                products = await _productRepository.Products
                    .Where(p => p.Title!.ToLower().Contains(q.ToLower()) || p.Description!.ToLower().Contains(q.ToLower()))
                    .ToListAsync();
            }

            if (id != null)
            {
                 products = await _productRepository.Products
                    .Where(p => p.CategoryId == id)
                    .ToListAsync();
            }

            var viewModel = new ProductListViewModel
            {
                Products = products,
                Categories = await _categoryRepository.Categories.ToListAsync()
            };
            return View(viewModel);
        }
    }
}