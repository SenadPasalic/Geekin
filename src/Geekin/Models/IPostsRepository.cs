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
                    TimePosted = o.TimePosted,
                    //LikeCounter = o.LikeCounter
                })
                .ToArray();
        }
        public void AddPost(AddPostVM viewModel, string postedBy)
        {
            if(viewModel.Link != null)
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

    }
}
