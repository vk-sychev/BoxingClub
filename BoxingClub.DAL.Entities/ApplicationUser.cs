using Microsoft.AspNetCore.Identity;
using System;

namespace BoxingClub.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public string Description { get; set; }


        public void ChangeUserProperties(ApplicationUser user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Patronymic = user.Patronymic;
            UserName = user.UserName;
            Description = user.Description;
        }
    }

}
