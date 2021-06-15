using System.Net.Http;
using System.Threading.Tasks;
using HttpClients.Models;
using IdentityModel.Client;

namespace HttpClients.Interfaces
{
    public interface IUserClient
    {
        Task<TokenResponse> GetTokenAsync(string username, string password);

        Task<HttpResponseMessage> SignUpAsync(SignUpModel model);

        Task<HttpResponseMessage> GetUsers(SearchModel searchModel, string token);

        Task<HttpResponseMessage> DeleteUser(string id, string token);

        Task<HttpResponseMessage> GetUser(string id, string token);

        Task<HttpResponseMessage> EditUser(string token, UserModel model);

        Task<HttpResponseMessage> GetRoles(string token);

        Task<HttpResponseMessage> GetUsersByRole(string token, string roleName);
    }
}
