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
    
        
    
    }
}