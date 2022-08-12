using System;
using System.ComponentModel.DataAnnotations;

namespace HttpClients.Models
{
    public class SignUpModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

    }
}
