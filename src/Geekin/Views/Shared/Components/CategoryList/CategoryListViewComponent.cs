using Geekin.Models;
using Geekin.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.Views.Shared.Components.CategoryList
{
    public class CategoryListViewComponent : ViewComponent
    {
        IPostsRepository _postsRepository;
        public CategoryListViewComponent(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }
        public IViewComponentResult Invoke()
        {
            //var model = _postsRepository.GetAllPosts();
            MasterOneVM model = new MasterOneVM();
            model.Categories = _postsRepository.GetAllCategories();
            return View(model);
        }
    }
}
