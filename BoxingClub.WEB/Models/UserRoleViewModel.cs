using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class UserRoleViewModel
    {
        public string RoleId { get; set; }
        
        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}
