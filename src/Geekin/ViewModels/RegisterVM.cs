using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Geekin.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username required.")]        
        public string UserName { get; set; }
    }
}
