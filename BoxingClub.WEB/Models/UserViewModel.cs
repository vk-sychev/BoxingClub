using System.ComponentModel;

namespace BoxingClub.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        public RoleViewModel Role { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Description { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return $"{Surname} {Name} {Patronymic}"; } }

        [DisplayName("Role")]
        public string RoleName { get { return (Role != null) ? Role.Name : ""; } }
    }
}
