using System;

namespace IdentityServer.BLL.Entities
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public RoleDTO Role { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public bool LockoutOnFailure { get; set; } = false;
    }
}
