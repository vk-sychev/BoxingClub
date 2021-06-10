using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Http;

namespace BoxingClub.Web.HttpClients.Interfaces
{
    public interface IAuthClient
    {
        Task<string> GetTokenAsync(string username, string password);

        Task<HttpResponseMessage> SignUpAsync(SignUpViewModel model);
    }
}
