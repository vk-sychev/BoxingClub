﻿namespace BoxingClub.DAL.Entities
{
    public class SignIn
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public bool LockoutOnFailure { get; set; } = false;
    }
}
