using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Geekin.ViewModels;
using Geekin.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Geekin.Controllers
{
    public class AdminController : Controller
    {
        IPostsRepository _postsRepository;
        UserManager<IdentityUser> userManager; //klassvariabel
        SignInManager<IdentityUser> signInManager; //klassvaria
        DBContext context;
        public AdminController(IPostsRepository postsRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            DBContext context)
        {
            _postsRepository = postsRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        // GET: /<controller>/
        //Admin/AddPost
        [Authorize]

        public IActionResult AddPost()
        {
            var model = new AddPostVM();
            model.Categories = _postsRepository.GetAllCategories();
            //var model = _postsRepository.GetAllPosts();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPost(AddPostVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _postsRepository.AddPost(viewModel, "Administrator");
            return RedirectToAction("index", "home");
        }

        //Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            //Skapa DB-schemat
            await context.Database.EnsureCreatedAsync();

            //Skapa användaren
            var result = await userManager.CreateAsync(
                new IdentityUser(viewModel.UserName), viewModel.Password);


            // Visa ev. fel-meddelande
            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(LoginVM.UserName),
                    result.Errors.First().Description);

                return View(viewModel);
            }

            //Logga in
            await signInManager.PasswordSignInAsync(
                viewModel.UserName, viewModel.Password, false, false);

            return RedirectToAction(nameof(HomeController.Index), "home"); //då du blivit inloggad, hoppa till Home/Index
        }
        //Logout
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "home");
        }
    }        
}
