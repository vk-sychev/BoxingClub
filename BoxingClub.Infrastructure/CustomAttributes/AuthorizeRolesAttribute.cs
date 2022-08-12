using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BoxingClub.Infrastructure.CustomAttributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Roles = string.Join(",", roles);
        }
    }
}
