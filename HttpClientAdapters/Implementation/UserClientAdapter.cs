using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Interfaces;
using HttpClients.Models;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace HttpClientAdapters.Implementation
{
    public class UserClientAdapter : IUserClientAdapter
    {
        private readonly IUserClient _userClient;

        public UserClientAdapter(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public async Task<TokenResponseModel> GetTokenAsync(string username, string password)
        {
            var response = await _userClient.GetTokenAsync(username, password);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return new TokenResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    AccessToken = response.AccessToken
                };
            }

            return new TokenResponseModel()
            {
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task<SignUpEditResponseModel> SignUpAsync(SignUpModel model)
        {
            var response = await _userClient.SignUpAsync(model);
            var errors = new List<AccountError>();

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                errors = JsonConvert.DeserializeObject<List<AccountError>>(content);
            }

            return new SignUpEditResponseModel()
            {
                StatusCode = response.StatusCode,
                Errors = errors
            };
        }

        public async Task<PageModelResponse<UserModel>> GetUsers(SearchModel searchModel, string token)
        {
            var response = await _userClient.GetUsers(searchModel, token);
            PageModel<UserModel> users = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<PageModel<UserModel>>(content);
            }

            return new PageModelResponse<UserModel>()
            {
                StatusCode = response.StatusCode,
                Items = users
            };
        }

        public async Task<HttpStatusCode> DeleteUser(string id, string token)
        {
            var response = await _userClient.DeleteUser(id, token);
            return response.StatusCode;
        }

        public async Task<ItemResponseModel<UserModel>> GetUser(string id, string token)
        {
            var response = await _userClient.GetUser(id, token);
            UserModel user = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UserModel>(content);
            }

            return new ItemResponseModel<UserModel>()
            {
                StatusCode = response.StatusCode,
                Item = user
            };
        }

        public async Task<SignUpEditResponseModel> EditUser(string token, UserModel model)
        {
            var response = await _userClient.EditUser(token, model);
            var errors = new List<AccountError>();

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                errors = JsonConvert.DeserializeObject<List<AccountError>>(content);
            }

            return new SignUpEditResponseModel()
            {
                StatusCode = response.StatusCode,
                Errors = errors
            };
        }

        public async Task<ItemsResponseModel<RoleModel>> GetRoles(string token)
        {
            var response = await _userClient.GetRoles(token);
            var roles = new List<RoleModel>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                roles = JsonConvert.DeserializeObject<List<RoleModel>>(content);
            }

            return new ItemsResponseModel<RoleModel>()
            {
                StatusCode = response.StatusCode,
                Items = roles
            };
        }

        public async Task<ItemsResponseModel<UserModel>> GetUsersByRole(string token, string roleName)
        {
            var response = await _userClient.GetUsersByRole(token, roleName);
            var users = new List<UserModel>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<UserModel>>(content);
            }

            return new ItemsResponseModel<UserModel>
            {
                StatusCode = response.StatusCode,
                Items = users
            };
        }

        public async Task<ItemResponseModel<UserModel>> GetUserByUsername(string token, string username)
        {
            var response = await _userClient.GetUserByUsername(token, username);

            UserModel user = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UserModel>(content);
            }

            return new ItemResponseModel<UserModel>()
            {
                StatusCode = response.StatusCode,
                Item = user
            };
        }
    }
}
