using Geekin.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Geekin.Models
{
    public interface IPostsRepository
    {
        PostListVM[] GetAll();
        void AddPost(AddPostVM viewModel, string postedBy);
        //AddCategoryVM GetRegisterEducationOptions();
        //void AddCategory(AddCategoryVM model);
    }

    public class DbPostsRepository : IPostsRepository
    {
        DBContext _context;

        public DbPostsRepository(DBContext context)
        {
            _context = context;
        }
        //Hämta alla poster från db
        public PostListVM[] GetAll()
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Select(o => new PostListVM
                {
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted,
                    //LikeCounter = o.LikeCounter
                })
                .ToArray();
        }
        //Skriv till db
        public void AddPost(AddPostVM viewModel, string postedBy)
        {
            if (viewModel.Link != null)
            {
                viewModel.Link = viewModel.Link.Replace("watch?v=", "embed/");
            }

            //var user = _context.Users.
            _context.Posts.Add(new Post
            {
                Title = viewModel.Title,
                Text = viewModel.mytextarea,
                Link = viewModel.Link,
                TimePosted = DateTime.Now,
                LikeCounter = 0
            });
            _context.SaveChanges();
        }
        //Lägg till nya kategorier till dropdown list
        //public AddCategoryVM GetRegisterEducationOptions()
        //{
        //    //Sätt kurser i en drodown list            
        //    var courseOptions = _context.Posts.Select(e =>
        //        new SelectListItem
        //        {
        //            Value = e.Id.ToString(),
        //            Text = $"{e.Category}"
        //        });
        //    //Sätt de nya listorna till vy modellen
        //    var categoryOptions = new AddCategoryVM()
        //    {
        //        Category = courseOptions
        //    };
        //    return categoryOptions;
        //}
        //public void AddCategory(AddCategoryVM model)
        //{
        //    Post category = new Post()
        //    {
        //        Category = model.Category
        //    };

        //    _context.Posts.Add(category);
        //    _context.SaveChanges();
        //}
    }
}
