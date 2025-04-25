using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.ViewModel;

namespace StoreApp.Controllers
{
    public class HomeController:Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(int? id)
        {
            var products = _productRepository.Products.ToList();


            if ( id != null)
            {
                products = products.Where(p=>p.CategoryId==id).ToList();
            }

            var viewModel = new ProductListViewModel {
                Products = products,
                Categories = _categoryRepository.Categories.ToList()
            };
            return View(viewModel);
        }
         public async Task<IActionResult> Details(int? id)
        {
            var product = await _productRepository.Products.FirstOrDefaultAsync(p=>p.ProductId==id);
            return View(product);
        }
    }
}