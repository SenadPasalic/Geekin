using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Geekin.ViewModels
{
    public class AddPostVM
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title required.")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [UIHint("tinymce_jquery_full")] //AllowHtml
        [Required(ErrorMessage = "Did you forget the content?")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        public DateTime TimePosted { get; set; }
    }
}
