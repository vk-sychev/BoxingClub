using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using HttpClients.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace HttpClients.Implementation
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserClient> _logger;
        private readonly string _baseUrl;
        private readonly string _administrationController = "Administration";
        private readonly string _accountController = "Account";
        private readonly string _clientId;
        private readonly string _clientSecret;

        public UserClient(HttpClient httpClient, 
                          IConfiguration configuration,
                          ILogger<UserClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "httpClient is null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "logger is null");
            _baseUrl = httpClient.BaseAddress?.ToString();

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "configuration is null");
            }

            _clientId = configuration.GetSection("AuthServer").GetSection("Credentials").GetSection("Client_Id").Value;
            _clientSecret = configuration.GetSection("AuthServer").GetSection("Credentials").GetSection("ClientSecret").Value;
        }

        public async Task<TokenResponse> GetTokenAsync(string username, string password)
        {
            var discoveryDocument = await GetDiscoveryDocument();
            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(
                new PasswordTokenRequest()
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                    UserName = username,
                    Password = password
                });

            if (tokenResponse.HttpStatusCode != HttpStatusCode.OK || tokenResponse.IsError)
            {
                _logger.LogError(tokenResponse.Error);
            }
            return tokenResponse;
        }

        public async Task<HttpResponseMessage> SignUpAsync(SignUpModel model)
        {
            var signUpUrl = $"{_baseUrl}{_accountController}/SignUp";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            var response = await _httpClient.PostAsync(signUpUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetUsers(SearchModel searchModel, string token)
        {
            var parameters = $"?PageIndex={searchModel.PageIndex}&PageSize={searchModel.PageSize}";
            var getUsersUrl = $"{_baseUrl}{_administrationController}/GetUsers{parameters}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getUsersUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteUser(string id, string token)
        {
            var deleteUserUrl = $"{_baseUrl}{_administrationController}/DeleteUser/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteUserUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetUser(string id, string token)
        {
            var getUserUrl = $"{_baseUrl}{_administrationController}/GetUser/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getUserUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> EditUser(string token, UserModel model)
        {
            _httpClient.SetBearerToken(token);
            var editUserUrl = $"{_baseUrl}{_administrationController}/EditUser/{model.Id}";
            var dictionary = GetModelDictionary(model);

            var content = new FormUrlEncodedContent(dictionary);
            var response = await _httpClient.PostAsync(editUserUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetRoles(string token)
        {
            var getRolesUrl = $"{_baseUrl}{_administrationController}/GetRoles";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getRolesUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetUsersByRole(string token, string roleName)
        {
            var getUsersByRoleUrl = $"{_baseUrl}{_administrationController}/GetUsersByRole/{roleName}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getUsersByRoleUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetUserByUsername(string token, string username)
        {
            var getUserByUsernameUrl = $"{_baseUrl}{_administrationController}/GetUserByUsername/{username}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getUserByUsernameUrl);

            return GetResponse(response);
        }


        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocument()
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(_baseUrl);
            if (discovery.HttpStatusCode != HttpStatusCode.OK || discovery.IsError)
            {
                _logger.LogError(discovery.Error);
                throw new InvalidOperationException("Invalid Sign In Attempt");
            }

            return discovery;
        }


        private Dictionary<string, string> GetModelDictionary(object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private Dictionary<string, string> GetModelDictionary(UserModel model)
        {
            var dictionary = new Dictionary<string, string>();

            dictionary.Add("Id", model.Id);
            dictionary.Add("UserName", model.UserName);
            dictionary.Add("RoleId", model.Role.Id);
            dictionary.Add("Name", model.Name);
            dictionary.Add("Surname", model.Surname);
            dictionary.Add("Patronymic", model.Patronymic);
            dictionary.Add("Description", model.Description);

            return dictionary;
        }

        private HttpResponseMessage GetResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }
    }
}
