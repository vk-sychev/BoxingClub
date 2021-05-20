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


        public static void ChangeUserProperties(ApplicationUser userFromDb, ApplicationUser user)
        {
            userFromDb.Name = user.Name;
            userFromDb.Surname = user.Surname;
            userFromDb.Patronymic = user.Patronymic;
            userFromDb.UserName = user.UserName;
            userFromDb.Description = user.Description;
        }
    }

}
