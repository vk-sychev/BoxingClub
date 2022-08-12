using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.DAL.Entities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }

        public ApplicationRole Role { get; set; }
    }
}
