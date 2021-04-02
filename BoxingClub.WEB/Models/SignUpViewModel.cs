using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class SignUpViewModel
    {
        //[Required]
        public string NickName { get; set; }

        //[Required]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
