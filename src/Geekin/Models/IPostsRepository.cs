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
            //var user = _context.Users.
        }
    }
}
