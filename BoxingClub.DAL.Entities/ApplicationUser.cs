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
    }
}
