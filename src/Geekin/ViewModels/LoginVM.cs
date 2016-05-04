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
        [Required(ErrorMessage = "Fyll i din e-postadress.")]
        public string UserName { get; set; }

        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Fyll i ditt lösenord.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
