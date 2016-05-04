using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Geekin.ViewModels;
using Geekin.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Geekin.Controllers
{
    public class AdminController : Controller
    {
        IPostsRepository _postsRepository;
        public AdminController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }
        // GET: /<controller>/
        //Admin/AddPost
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(AddPostVM viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _postsRepository.AddPost(viewModel, "Administrator");
            return RedirectToAction("index", "home");
        }
    }
}
