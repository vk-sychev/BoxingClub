using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.Web.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace BoxingClub.Web.HttpClients.Interfaces
{
    public interface IUserClient
    {
        Task<TokenResponse> GetTokenAsync(string username, string password);

        Task<HttpResponseMessage> SignUpAsync(SignUpViewModel model);

        Task<HttpResponseMessage> GetUsers(SearchModelDTO searchModel, string token);

        Task<HttpResponseMessage> DeleteUser(string id, string token);

        Task<HttpResponseMessage> GetUser(string id, string token);

        Task<HttpResponseMessage> EditUser(string id, string token, UserViewModel model);

        Task<HttpResponseMessage> GetRoles(string token);
    }
}
