using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace BoxingClub.Web.CustomAttributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme;
            Roles = string.Join(",", roles);
        }
    }
}
