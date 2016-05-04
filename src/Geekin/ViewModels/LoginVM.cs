using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail required.")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
