using System.ComponentModel.DataAnnotations;

namespace BoxingClub.Web.Models
{
    public class SignInViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
