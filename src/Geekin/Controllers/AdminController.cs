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
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Geekin.Controllers
{
    public class AdminController : Controller
    {
        IPostsRepository _postsRepository;
        UserManager<IdentityUser> userManager; //klassvariabel
        SignInManager<IdentityUser> signInManager; //klassvaria
        DBContext context;
        RoleManager<IdentityRole> roleManager;

        public AdminController(IPostsRepository postsRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            DBContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _postsRepository = postsRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.roleManager = roleManager;
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
        public IActionResult AddPost(AddPostVM viewModel, string command)
        {
            if (command.Equals("submit2"))
            {
                _postsRepository.AddNewCategory(viewModel);
                return RedirectToAction("addpost", "admin");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                _postsRepository.AddPost(viewModel, "Administrator");
                return RedirectToAction("index", "home");
            }
        }

        //Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            //Skapa DB-schemat
            await context.Database.EnsureCreatedAsync();

            //Skapa användaren
            var identityUser = new IdentityUser { Email = viewModel.Email, UserName = viewModel.UserName };
            var result = await userManager.CreateAsync(identityUser, viewModel.Password);



            // Visa ev. fel-meddelande
            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(LoginVM.UserName),
                    result.Errors.First().Description);

                return View(viewModel);
            }

            await signInManager.PasswordSignInAsync(
                viewModel.UserName, viewModel.Password, false, false);

            //Skapa Admin roll
            //var roleResult = await roleManager.CreateAsync(new IdentityRole("User"));
            //if (roleResult.Succeeded)
            //{
                var user = await userManager.FindByNameAsync(viewModel.UserName);
                //var userResult = await userManager.AddToRoleAsync(user, "User");
            await userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), "Admin"));
            //}

            //Logga in

            await context.SaveChangesAsync();

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
