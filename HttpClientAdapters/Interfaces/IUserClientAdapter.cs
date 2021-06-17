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

        Task<PageModelResponse<UserModel>> GetUsers(SearchModel searchModel, string token);

        Task<HttpStatusCode> DeleteUser(string id, string token);

        Task<ItemResponseModel<UserModel>> GetUser(string id, string token);

        Task<SignUpEditResponseModel> EditUser(string token, UserModel model);

        Task<ItemsResponseModel<RoleModel>> GetRoles(string token);

        Task<ItemsResponseModel<UserModel>> GetUsersByRole(string token, string roleName);

        Task<ItemResponseModel<UserModel>> GetUserByUsername(string token, string username);
    }
}
