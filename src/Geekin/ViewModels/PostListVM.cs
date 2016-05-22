using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.ViewModels
{
    public class PostListVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string mytextarea { get; set; }
        public string Link { get; set; }
        public string PostedByFirstname { get; set; }
        public string PostedByLastname { get; set; }
        public DateTime TimePosted { get; set; }
        public int LikeCounter { get; set; }

        public string Category { get; set; }

        public AddCategoryVM[] Categories { get; set; }
    }
}
