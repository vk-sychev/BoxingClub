using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HttpClients.Interfaces;
using HttpClients.Models;
using HttpClients.Models.SpecModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Routing;
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
        private readonly string _medicalCertificateController = "MedicalCertificate";

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

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetBoxingGroups(string token)
        {
            var getAllBoxingGroupsUrl = $"{_baseUrl}{_homeController}/GetAllBoxingGroups";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getAllBoxingGroupsUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetBoxingGroup(string token, int id)
        {
            var getBoxingGroupUrl = $"{_baseUrl}{_homeController}/GetBoxingGroup/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getBoxingGroupUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetBoxingGroupWithStudents(string token, int id)
        {
            var getBoxingGroupWithStudentsUrl = $"{_baseUrl}{_homeController}/GetBoxingGroupWithStudents/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getBoxingGroupWithStudentsUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteBoxingGroup(string token, int id)
        {
            var deleteBoxingGroupUrl = $"{_baseUrl}{_homeController}/DeleteBoxingGroup/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteBoxingGroupUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> EditBoxingGroup(string token, BoxingGroupLiteModel model)
        {
            var editBoxingGroupUrl = $"{_baseUrl}{_homeController}/EditBoxingGroup/{model.Id}";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editBoxingGroupUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> CreateBoxingGroup(string token, BoxingGroupLiteModel model)
        {
            var createBoxingGroupUrl = $"{_baseUrl}{_homeController}/CreateBoxingGroup";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(createBoxingGroupUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteStudentFromBoxingGroup(string token, int studentId)
        {
            var deleteStudentFromBoxingGroup = $"{_baseUrl}{_homeController}/DeleteFromBoxingGroup/{studentId}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteStudentFromBoxingGroup);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetStudents(string token, SearchModel searchModel)
        {
            var parameters = $"?PageIndex={searchModel.PageIndex}&PageSize={searchModel.PageSize}&ExperienceFilter={searchModel.ExperienceFilter}&MedExaminationFilter={searchModel.MedExaminationFilter}";
            var getStudentsUrl = $"{_baseUrl}{_studentController}/GetStudents{parameters}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getStudentsUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetStudentsBySpecification(string token, TournamentWithSpecification tournamentWithSpecification)
        {
            var json = JsonConvert.SerializeObject(tournamentWithSpecification);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var getStudentsBySpecificationUrl =
                $"{_baseUrl}{_studentController}/GetStudentsBySpecification";

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(getStudentsBySpecificationUrl),
                Content = content
            };

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.SendAsync(request);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetStudentsByIds(string token, List<int> ids)
        {
            var parameters = GetParametersFromList(ids);
            var getStudentsByIds = $"{_baseUrl}{_studentController}/GetStudentsByIds?{parameters}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getStudentsByIds);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetStudent(string token, int id)
        {
            var getStudentUrl = $"{_baseUrl}{_studentController}/GetStudent/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getStudentUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> CreateStudent(string token, StudentFullModel model)
        {
            var createStudentUrl = $"{_baseUrl}{_studentController}/CreateStudent";

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(createStudentUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> EditStudent(string token, StudentFullModel model)
        {
            var editStudentUrl = $"{_baseUrl}{_studentController}/EditStudent/{model.Id}";

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editStudentUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteStudent(string token, int id)
        {
            var deleteStudentUrl = $"{_baseUrl}{_studentController}/DeleteStudent/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteStudentUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> GetMedicalCertificate(string token, int id)
        {
            var getMedicalCertificateUrl = $"{_baseUrl}{_medicalCertificateController}/GetMedicalCertificate/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.GetAsync(getMedicalCertificateUrl);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> CreateMedicalCertificate(string token, MedicalCertificateModel model)
        {
            var createMedicalCertificateUrl = $"{_baseUrl}{_medicalCertificateController}/CreateMedicalCertificate";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(createMedicalCertificateUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> EditMedicalCertificate(string token, MedicalCertificateModel model)
        {
            var editMedicalCertificateUrl = $"{_baseUrl}{_medicalCertificateController}/EditMedicalCertificate/{model.Id}";

            var dictionary = GetModelDictionary(model);
            var content = new FormUrlEncodedContent(dictionary);

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.PostAsync(editMedicalCertificateUrl, content);

            return GetResponse(response);
        }

        public async Task<HttpResponseMessage> DeleteMedicalCertificate(string token, int id)
        {
            var deleteMedicalCertificateUrl = $"{_baseUrl}{_medicalCertificateController}/DeleteMedicalCertificate/{id}";

            _httpClient.SetBearerToken(token);
            var response = await _httpClient.DeleteAsync(deleteMedicalCertificateUrl);

            return GetResponse(response);
        }


        private Dictionary<string, string> GetModelDictionary(object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        private HttpResponseMessage GetResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(response.ReasonPhrase);
            }

            return response;
        }

        private string GetParametersFromList(List<int> ids)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < ids.Count; i++)
            {
                sb.Append($"ids[{i}]={ids[i]}");

                if (i != ids.Count - 1)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
    }
}
