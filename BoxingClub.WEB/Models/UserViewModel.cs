namespace BoxingClub.WEB.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public RoleViewModel Role { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Description { get; set; }

        public string FullName { get { return $"{Surname} {Name} {Patronymic}"; } }
    }
}
