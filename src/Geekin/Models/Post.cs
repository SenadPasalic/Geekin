using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        //public string PostedByFirstName { get; set; }
        //public string PostedByLastName { get; set; }
        public DateTime TimePosted { get; set; }
        //public string UserID { get; set; }
        //public virtual User User { get; set; }
    }
}
