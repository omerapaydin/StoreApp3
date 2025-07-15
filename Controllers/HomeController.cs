using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApp3.Data.Abstract;
using StoreApp3.Entity;
using StoreApp3.ViewModel;

namespace StoreApp3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(IProductRepository productRepository,UserManager<ApplicationUser> userManager, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int? id, string? q, string? sort)
        {
            var products = await _productRepository.Products.ToListAsync();

            if (!string.IsNullOrEmpty(q))
            {
                products = await _productRepository.Products
                    .Where(p => p.Title!.ToLower().Contains(q.ToLower()) || p.Description!.ToLower().Contains(q.ToLower()))
                    .ToListAsync();
            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "priceDesc")
                {
                    products = products.OrderByDescending(p => p.Price).ToList();
                }
                else if (sort == "priceAsc")
                {
                    products = products.OrderBy(p => p.Price).ToList();
                }
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
   
         public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model, IFormFile imageFile)
        {
            
                var extension = "";

            if (imageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                extension = Path.GetExtension(imageFile.FileName);

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Geçerli bir resim seçiniz");
                    return View(model);
                }
            }
                else
                {
                    ModelState.AddModelError("", "Lütfen bir resim dosyası seçiniz");
                    return View(model); 
                }


            if (ModelState.IsValid)
            {

                var randomFileName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                  using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                if (User.Identity!.IsAuthenticated)
                {
                    var product = new Product
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Image = randomFileName,
                        PublishedOn = DateTime.Now,
                        Price = model.Price,
                        UserId = _userManager.GetUserId(User)!,
                        CategoryId = model.CategoryId
                    };

                    _productRepository.AddProduct(product);
                    return RedirectToAction("List", "Home");
                }
            }
            return View(model);
        }
   
   
    }
}