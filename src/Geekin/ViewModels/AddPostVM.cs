using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Geekin.ViewModels
{
    public class AddPostVM
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title required.")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Did you forget the content?")]
        [DataType(DataType.MultilineText)]
        [UIHint("tinymce_jquery_full")] //AllowHtml
        public string Text { get; set; }

        [Display(Name = "Picture link")]
        public string Image { get; set; }

        [Display(Name = "YouTube link")]
        public string Link { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        public DateTime TimePosted { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Did you forget the category?")]
        public string Category { get; set; }


        public string PostedBy { get; set; }

        public AddCategoryVM[] Categories { get; set; }

        public string NewCategory { get; set; }
    }
}
