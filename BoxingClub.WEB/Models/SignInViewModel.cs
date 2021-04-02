using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class SignInViewModel
    {
        //[Required]
        public string NickName { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
