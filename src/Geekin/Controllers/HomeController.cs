using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Geekin.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Geekin.Models;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
//using System.Web.Security;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Geekin.Controllers
{
    public class HomeController : Controller
    {
        UserManager<IdentityUser> userManager; //klassvariabel
        SignInManager<IdentityUser> signInManager; //klassvariabel
        IdentityDbContext context; //klassvariabel      
        IPostsRepository repository;
        DBContext dbContext;

        //Konstruktor
        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IdentityDbContext context,
            IPostsRepository repository,
            DBContext dbContext)
        {
            this.userManager = userManager; //sätter klassvariabeln
            this.signInManager = signInManager; //sätter klassvariabeln
            this.context = context; //sätter klassvariabeln 
            this.repository = repository;
            this.dbContext = dbContext;
        } 

        // GET: /<controller>/
        //Home/Index
        public async Task<IActionResult> Index(string search)
        {
            //var model = repository.GetAllPosts();

            MasterOneVM model = new MasterOneVM();

            //if (User.Identity.IsAuthenticated)
            //{
            //    var user = await userManager.FindByNameAsync(User.Identity.Name);
            //    model.IsUserAdmin = await userManager.IsInRoleAsync(user, "Admin");
            //}

            var posts = from m in dbContext.Posts
                         select m;
            
            //Search
            if (!String.IsNullOrEmpty(search))
            {
                model.BlogPosts = repository.GetSearch(search);                                    
            }
            else
            {                
                model.BlogPosts = repository.GetAllPosts();
            }
                model.Categories = repository.GetAllCategories();

            //var model = new PostListVM();
            //model.Categories = repository.GetAllCategories();
            //return View(model);

            return View(model);
        }
        //Admin
        public IActionResult Admin()
        {
            return View();
        }

        //för att visa index 2
        public IActionResult Index2()
        {
            var model = repository.GetAllPosts();
            return View(model);
        }


        //BlogPost
        public IActionResult BlogPost(int myTitle)
        {
            //var model = repository.GetOnePost(myTitle);

            MasterOneVM model = new MasterOneVM();
            model.BlogPosts = repository.GetOnePost(myTitle);
            model.Categories = repository.GetAllCategories();

            return View(model);
        }
        //Category
        public IActionResult Category(string myCategory)
        {
            var model = repository.SelectCategory(myCategory);
            return View(model);
        }
        //Tags
        public IActionResult Tags(int myTag)
        {
            var model = repository.SelectTag(myTag);
            return View(model);
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
            //await context.Database.EnsureCreatedAsync();

            // Skapa användaren
            //var result = await userManager.CreateAsync(
            //new IdentityUser(viewModel.UserName), viewModel.Password);

            // Visa ev. fel-meddelande
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError(nameof(LoginVM.UserName),
            //        result.Errors.First().Description);

            //    return View(viewModel);
            //}
                        //Skapa DB-schemat
                        //await context.Database.EnsureCreatedAsync();

                        ////Skapa användaren
                        //var result = await userManager.CreateAsync(
                        //    new IdentityUser(viewModel.UserName), viewModel.Password);


                        // Visa ev. fel-meddelande
                        //if (!result.Succeeded)
                        //{
                        //    ModelState.AddModelError(nameof(LoginVM.UserName),
                        //        result.Errors.First().Description);

                        //    return View(viewModel);
                        //}

                        //Logga in
                        await signInManager.PasswordSignInAsync(
                            viewModel.UserName, viewModel.Password, false, false);


           // var user2 = dbContext.Users.Single(o => o.UserName == viewModel.UserName); //namnet på den inloggade
            //var currentUser = dbContext.Users.Single(o => o.Id == viewModel.UserName); //namnet på den inloggade
            var user = User.Identity.Name; //namnet på den inloggade
            //var userId = await userManager.FindByIdAsync(User.Identity.GetUserId());
            //string currentUserId = User.Identity.GetUserId();

            //Roles.AddUserToRole(user, "Admin");

            //Sätt role
            //var currentUser = userManager.FindByNameAsync(user);
            //var roleresult = userManager.AddToRoleAsync(currentUser.Id, "Admin");



            return RedirectToAction(nameof(HomeController.Index)); //då du blivit inloggad, hoppa till ... //.Index


            //var owner = User.Identity.Name; //namnet på den inloggade
            //var user = dbContext.Users.Single(o => o.UserName == viewModel.UserName);

            //if (user.RegistrationComplete)
            //{
            //    var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            //    //om admin eller lärare loggar in, skall de få en annan view
            //    //if (await userManager.IsInRoleAsync(aspUser, "Admin"))
            //    //{
            //    //    return RedirectToAction(nameof(AdminController.Index), "Admin");
            //    //}
            //    //else if (await userManager.IsInRoleAsync(aspUser, "Lärare"))
            //    //{
            //    //    return RedirectToAction(nameof(TeacherController.Index), "Teacher");
            //    //}
            //    if (result.Succeeded)
            //        return RedirectToAction(nameof(HomeController.Index), "home");
            //    else
            //    {
            //        ModelState.AddModelError(nameof(LoginVM.UserName), "FEEEL");
            //        return View(viewModel);
            //    }

            //}

            //else
            //{
            //    ModelState.AddModelError(nameof(LoginVM.UserName), "Du har inte fullföljt registreringen");
            //    return View(viewModel);
            //}
        }

        //Delete BlogPost
        [HttpPost]
        public IActionResult DeleteBlogPost(int postId)
        {
            repository.DeleteBlogPost(postId);
            return RedirectToAction(nameof(HomeController.Index));
        }

        //Edit BlogPost
        [HttpPost]
        public IActionResult EditBlogPost(int postId)
        {
            var model = repository.GetAllPosts().Where(o => o.Id == postId)
                .Select(o => new AddPostVM
                {
                    Id = postId,
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link
                })
                .Single();

            return View(model);
        }

        //Update BlogPost
        [HttpPost]
        public IActionResult UpdateBlogPost(PostListVM model)
        {
            repository.UpdateBlogPost(model);
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
