using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoxingClub.Web.HttpClients.Interfaces;
using IdentityModel.Client;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.Web.HttpClients.Implementation
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthClient> _logger;
        private readonly string _baseUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthClient(HttpClient httpClient, 
                          IConfiguration configuration,
                          ILogger<AuthClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "httpClient is null");
            _logger = logger;
            _baseUrl = _httpClient.BaseAddress.ToString();

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "configuration is null");
            }

            _clientId = configuration.GetSection("AuthServer").GetSection("Credentials").GetSection("Client_Id").Value;
            _clientSecret = configuration.GetSection("AuthServer").GetSection("Credentials").GetSection("ClientSecret").Value;
        }

        public async Task<string> GetTokenAsync(string username, string password)
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
                throw new InvalidOperationException("Invalid Sign In Attempt");
            }
            return tokenResponse.AccessToken;
        }

        public async Task<HttpResponseMessage> SignUpAsync(SignUpViewModel model)
        {
            var signUpUrl = $"{_httpClient.BaseAddress}Account/SignUp";
            var dictionary = GetSignUpModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);
            var response = await _httpClient.PostAsync(signUpUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
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

        private Dictionary<string, string> GetSignUpModelDictionary(SignUpViewModel model)
        {
            return new Dictionary<string, string>()
            {
                {"UserName", model.UserName},
                {"Name", model.Name},
                {"Surname", model.Surname},
                {"Patronymic", model.Patronymic},
                {"Password", model.Password},
                {"BornDate", model.BornDate.ToString("d")},
                {"Email", model.Email},
            };
        }
    }
}
