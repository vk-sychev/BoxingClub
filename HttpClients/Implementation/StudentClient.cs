using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using HttpClients.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace HttpClients.Implementation
{
    public class StudentClient : IStudentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StudentClient> _logger;
        private readonly string _baseUrl;
        private readonly string _homeController = "Home";
        private readonly string _studentController = "Student";

        public StudentClient(HttpClient httpClient,
                             ILogger<StudentClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "httpClient is null");
            _baseUrl = httpClient.BaseAddress?.ToString();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "logger is null");
        }

        public async Task<HttpResponseMessage> GetBoxingGroups(string token, SearchModel searchModel)
        {
            var parameters = $"?PageIndex={searchModel.PageIndex}&PageSize={searchModel.PageSize}";
            var getBoxingGroupsUrl = $"{_baseUrl}{_homeController}/GetBoxingGroups{parameters}";

            _httpClient.SetBearerToken(token);

            var response = await _httpClient.GetAsync(getBoxingGroupsUrl);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> GetBoxingGroups(string token)
        {
            var getAllBoxingGroupsUrl = $"{_baseUrl}{_homeController}/GetAllBoxingGroups";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getAllBoxingGroupsUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> GetBoxingGroup(string token, int id)
        {
            var getBoxingGroupUrl = $"{_baseUrl}{_homeController}/GetBoxingGroup/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getBoxingGroupUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> GetBoxingGroupWithStudents(string token, int id)
        {
            var getBoxingGroupWithStudentsUrl = $"{_baseUrl}{_homeController}/GetBoxingGroupWithStudents/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getBoxingGroupWithStudentsUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteBoxingGroup(string token, int id)
        {
            var deleteBoxingGroupUrl = $"{_baseUrl}{_homeController}/DeleteBoxingGroup/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteBoxingGroupUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> EditBoxingGroup(string token, BoxingGroupLiteModel model)
        {
            var editBoxingGroupUrl = $"{_baseUrl}{_homeController}/EditBoxingGroup/{model.Id}";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editBoxingGroupUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> CreateBoxingGroup(string token, BoxingGroupLiteModel model)
        {
            var createBoxingGroupUrl = $"{_baseUrl}{_homeController}/CreateBoxingGroup";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(createBoxingGroupUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteStudentFromBoxingGroup(string token, int studentId)
        {
            var deleteStudentFromBoxingGroup = $"{_baseUrl}{_homeController}/DeleteFromBoxingGroup/{studentId}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteStudentFromBoxingGroup);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> GetStudents(string token, SearchModel searchModel)
        {
            var parameters = $"?PageIndex={searchModel.PageIndex}&PageSize={searchModel.PageSize}&ExperienceFilter={searchModel.ExperienceFilter}&MedExaminationFilter={searchModel.MedExaminationFilter}";
            var getStudentsUrl = $"{_baseUrl}{_studentController}/GetStudents{parameters}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getStudentsUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }


        public async Task<HttpResponseMessage> GetStudent(string token, int id)
        {
            var getStudentUrl = $"{_baseUrl}{_studentController}/GetStudent/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getStudentUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> CreateStudent(string token, StudentFullModel model)
        {
            var createStudentUrl = $"{_baseUrl}{_studentController}/CreateStudent";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(createStudentUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> EditStudent(string token, StudentFullModel model)
        {
            var editStudentUrl = $"{_baseUrl}{_studentController}/EditStudent/{model.Id}";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary); //double cannot be decoded in students api

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editStudentUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteStudent(string token, int id)
        {
            var deleteStudentUrl = $"{_baseUrl}{_studentController}/DeleteStudent/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteStudentUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }


        private Dictionary<string, string> GetModelDictionary(object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
