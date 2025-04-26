using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Entity;
using StoreApp.ViewModel;

namespace StoreApp.Controllers
{
    public class AccountController:Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);

                if(user != null)
                {
                    await _signInManager.SignOutAsync();

                   await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);

                   return RedirectToAction("Index","Home");

                    
                }else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                }

            }else {
                ModelState.AddModelError("", "hatalı eposta ya da password");
            }
            return View(model);
        }
    
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model,IFormFile? imageFile)
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



            if(ModelState.IsValid)
            {

                var randomFileName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }


                var user = new ApplicationUser 
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    ImageFile = model.imageFile
                };

                var hasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = hasher.HashPassword(user,model.Password);

                await _userManager.CreateAsync(user);

                return RedirectToAction("Login", "Account");

            }
            return View(model);
        }
    
    }
}