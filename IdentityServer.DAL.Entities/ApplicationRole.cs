using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.DAL.Entities
{
    public class ApplicationRole : IdentityRole<string>
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}
