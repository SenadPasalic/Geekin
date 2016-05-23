using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.ViewModels
{
    public class MasterOneVM
    {
        public PostListVM[] BlogPosts { get; set; }
        public AddCategoryVM[] Categories { get; set; }
    }
}
