using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Geekin.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Geekin.Controllers
{
    public class HomeController : Controller
    {
        UserManager<IdentityUser> userManager; //klassvariabel
        SignInManager<IdentityUser> signInManager; //klassvariabel
        IdentityDbContext context; //klassvariabel      

        //Konstruktor
        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IdentityDbContext context)
        {
            this.userManager = userManager; //sätter klassvariabeln
            this.signInManager = signInManager; //sätter klassvariabeln
            this.context = context; //sätter klassvariabeln 
        } 

        // GET: /<controller>/
        //Home/Index
        public IActionResult Index()
        {
            return View();
        }
        //Admin
        public IActionResult Admin()
        {
            return View();
        }
        //Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Skapa DB-schemat
            await context.Database.EnsureCreatedAsync();

            // Skapa användaren
            var result = await userManager.CreateAsync(
                new IdentityUser(viewModel.UserName), viewModel.Password);

            // Visa ev. fel-meddelande
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError(nameof(LoginVM.UserName),
            //        result.Errors.First().Description);
                
            //    return View(viewModel);
            //}

            await signInManager.PasswordSignInAsync(
                viewModel.UserName, viewModel.Password, false, false);

            return RedirectToAction(nameof(HomeController.Index)); //då du blivit inloggad, hoppa till ... //.Index
        }
    }
}
