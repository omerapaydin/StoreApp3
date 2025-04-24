using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;

namespace StoreApp.Controllers
{
    public class HomeController:Controller
    {
        private IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            return View(await _productRepository.Products.ToListAsync());
        }
         public async Task<IActionResult> Details(int? id)
        {
            var product = await _productRepository.Products.FirstOrDefaultAsync(p=>p.ProductId==id);
            return View();
        }
    }
}