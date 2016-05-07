using Geekin.ViewModels;
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
    }

    public class DbPostsRepository : IPostsRepository
    {
        DBContext _context;

        public DbPostsRepository(DBContext context)
        {
            _context = context;
        }
        public PostListVM[] GetAll()
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Select(o => new PostListVM
                {
                    Title = o.Title,
                    Text = o.Text,
                    Link = o.Link,
                    TimePosted = o.TimePosted
                })
                .ToArray();
        }
        public void AddPost(AddPostVM viewModel, string postedBy)
        {
            if(viewModel.Link != null)
            {
                viewModel.Link = viewModel.Link.Replace("watch?v=", "v/");
            }


            //var user = _context.Users.
            _context.Posts.Add(new Post
            {
                Title = viewModel.Title,
                Text = viewModel.mytextarea,
                Link = viewModel.Link,
                TimePosted = DateTime.Now
            });
            _context.SaveChanges();
        }
    }
}
