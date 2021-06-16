using System.Net;
using System.Threading.Tasks;
using HttpClientAdapters.Models;
using HttpClients.Models;

namespace HttpClientAdapters.Interfaces
{
    public interface IUserClientAdapter
    {
        Task<TokenResponseModel> GetTokenAsync(string username, string password);

        Task<SignUpEditResponseModel> SignUpAsync(SignUpModel model);

        Task<PageModelResponse> GetUsers(SearchModel searchModel, string token);

        Task<HttpStatusCode> DeleteUser(string id, string token);

        Task<UserResponseModel> GetUser(string id, string token);

        Task<SignUpEditResponseModel> EditUser(string token, UserModel model);

        Task<RolesResponseModel> GetRoles(string token);

        Task<UsersResponseModel> GetUsersByRole(string token, string roleName);

        Task<UserResponseModel> GetUserByUsername(string token, string username);
    }
}
