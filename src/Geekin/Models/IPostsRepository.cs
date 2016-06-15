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
        PostListVM[] GetAllPosts();
        PostListVM[] GetOnePost(int myTitle);
        PostListVM[] SelectCategory(string myCategory);
        PostListVM[] SelectTag(int myTag);
        AddCategoryVM[] GetAllCategories();
        void AddPost(AddPostVM viewModel, string postedBy);
        void AddNewCategory(AddPostVM viewModel);
        void UpdateBlogPost(PostListVM model);
        void DeleteBlogPost(int postId);
        PostListVM[] GetSearch(string search);
    }

    public class DbPostsRepository : IPostsRepository
    {
        DBContext _context;

        public DbPostsRepository(DBContext context)
        {
            _context = context;
        }
        //Hämta alla poster från db
        public PostListVM[] GetAllPosts()
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Select(o => new PostListVM
                {
                    Id = o.Id,
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted,
                    Category = o.Category,
                    PostedBy = o.PostedBy,
                    Tags = o.Tags
                    //LikeCounter = o.LikeCounter
                })
                .ToArray();
        }
        //Hämta alla poster från en kategori
        public PostListVM[] SelectCategory(string myCategory)
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Where(o => o.Category == myCategory)
                .Select(o => new PostListVM
                {
                    Id = o.Id,
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted,
                    Category = o.Category,
                    PostedBy = o.PostedBy
                    //LikeCounter = o.LikeCounter
                })
                .ToArray();
        }
        //Hämta alla poster från en tag
        public PostListVM[] SelectTag(int myTag)
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Where(o => o.Id == myTag)
                .Select(o => new PostListVM
                {
                    Id = o.Id,
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted,
                    Category = o.Category,
                    PostedBy = o.PostedBy,
                    Tags = o.Tags
                    //LikeCounter = o.LikeCounter
                })
                .ToArray();
        }
        //Hämta en post från db
        public PostListVM[] GetOnePost(int myTitle)
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Where(o => o.Id == myTitle)
                .Select(o => new PostListVM
                {
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted,
                    //LikeCounter = o.LikeCounter
                    Category = o.Category,
                    PostedBy = o.PostedBy
                })
                .ToArray();
        }
        //Hämta alla kategorier från db 
        public AddCategoryVM[] GetAllCategories()
        {
            return _context.Category
                .OrderBy(o => o.CategoryName)
                .Select(o => new AddCategoryVM
                {
                    Id = o.Id,
                    CategoryName = o.CategoryName
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
                Text = viewModel.Text,
                Link = viewModel.Link,
                TimePosted = DateTime.Now,
                Category = viewModel.Category,
                LikeCounter = 0,
                PostedBy = viewModel.PostedBy,
                Tags = viewModel.Tags
            });
            _context.SaveChanges();
        }
        //Add new category
        public void AddNewCategory(AddPostVM viewModel)
        {
            _context.Category.Add(new Category
            {
                CategoryName = viewModel.NewCategory
            });
            _context.SaveChanges();
        }
        //Edit BlogPost
        public void UpdateBlogPost(PostListVM model)
        {
            var post = _context.Posts.Single(o => o.Id == model.Id);
            post.Title = model.Title;
            post.Text = model.Text;
            post.Link = model.Link;
            _context.SaveChanges();
        }
        //Delete BlogPost
        public void DeleteBlogPost(int postId)
        {
            var removeThisPost = _context.Posts.Single(o => o.Id == postId);
            _context.Posts.Remove(removeThisPost);
            _context.SaveChanges();
        }

        //Get Search
        public PostListVM[] GetSearch(string search)
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Where(o => o.Title.Contains(search)                
                        || o.Text.Contains(search)
                        || o.Category.Contains(search))
                .Select(o => new PostListVM
                {
                    Id = o.Id,
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted,
                    //LikeCounter = o.LikeCounter
                    Category = o.Category
                })
                .ToArray();
        }
    }
}
