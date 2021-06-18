using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpClientAdapters.Interfaces;
using HttpClientAdapters.Models;
using HttpClients.Interfaces;
using HttpClients.Models;
using HttpClients.Models.SpecModels;
using Newtonsoft.Json;

namespace HttpClientAdapters.Implementation
{
    public class StudentClientAdapter : IStudentClientAdapter
    {
        private readonly IStudentClient _studentClient;

        public StudentClientAdapter(IStudentClient studentClient)
        {
            _studentClient = studentClient;
        }

        public async Task<PageModelResponse<BoxingGroupLiteModel>> GetBoxingGroups(string token, SearchModel searchModel)
        {
            var response = await _studentClient.GetBoxingGroups(token, searchModel);
            PageModel<BoxingGroupLiteModel> boxingGroups = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                boxingGroups = JsonConvert.DeserializeObject<PageModel<BoxingGroupLiteModel>>(content);
            }

            return new PageModelResponse<BoxingGroupLiteModel>()
            {
                StatusCode = response.StatusCode,
                Items = boxingGroups
            };
        }

        public async Task<ItemsResponseModel<BoxingGroupLiteModel>> GetBoxingGroups(string token)
        {
            var response = await _studentClient.GetBoxingGroups(token);
            var boxingGroups = new List<BoxingGroupLiteModel>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                boxingGroups = JsonConvert.DeserializeObject<List<BoxingGroupLiteModel>>(content);
            }

            return new ItemsResponseModel<BoxingGroupLiteModel>()
            {
                StatusCode = response.StatusCode,
                Items = boxingGroups
            };
        }

        public async Task<ItemResponseModel<BoxingGroupLiteModel>> GetBoxingGroup(string token, int id)
        {
            var response = await _studentClient.GetBoxingGroup(token, id);
            BoxingGroupLiteModel boxingGroup = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                boxingGroup = JsonConvert.DeserializeObject<BoxingGroupLiteModel>(content);
            }

            return new ItemResponseModel<BoxingGroupLiteModel>()
            {
                StatusCode = response.StatusCode,
                Item = boxingGroup
            };
        }

        public async Task<ItemResponseModel<BoxingGroupFullModel>> GetBoxingGroupWithStudents(string token, int id)
        {
            var response = await _studentClient.GetBoxingGroupWithStudents(token, id);
            BoxingGroupFullModel boxingGroup = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                boxingGroup = JsonConvert.DeserializeObject<BoxingGroupFullModel>(content);
            }

            return new ItemResponseModel<BoxingGroupFullModel>()
            {
                StatusCode = response.StatusCode,
                Item = boxingGroup
            };
        }

        public async Task<HttpStatusCode> DeleteBoxingGroup(string token, int id)
        {
            var response = await _studentClient.DeleteBoxingGroup(token, id);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditBoxingGroup(string token, BoxingGroupLiteModel model)
        {
            var response = await _studentClient.EditBoxingGroup(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> CreateBoxingGroup(string token, BoxingGroupLiteModel model)
        {
            var response = await _studentClient.CreateBoxingGroup(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteStudentFromBoxingGroup(string token, int studentId)
        {
            var response = await _studentClient.DeleteStudentFromBoxingGroup(token, studentId);
            return response.StatusCode;
        }

        public async Task<PageModelResponse<StudentLiteModel>> GetStudents(string token, SearchModel searchModel)
        {
            var response = await _studentClient.GetStudents(token, searchModel);
            PageModel<StudentLiteModel> students = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                students = JsonConvert.DeserializeObject<PageModel<StudentLiteModel>>(content);
            }

            return new PageModelResponse<StudentLiteModel>()
            {
                StatusCode = response.StatusCode,
                Items = students
            };
        }

        public async Task<ItemsResponseModel<StudentFullModel>> GetStudentsBySpecification(string token,
            Tournament tournament, TournamentSpecification specification)
        {
            var response = await _studentClient.GetStudentsBySpecification(token, tournament, specification);
            var students = new List<StudentFullModel>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                students = JsonConvert.DeserializeObject<List<StudentFullModel>>(content);
            }

            return new ItemsResponseModel<StudentFullModel>()
            {
                StatusCode = response.StatusCode,
                Items = students
            };
        }

        public async Task<ItemsResponseModel<StudentFullModel>> GetStudentsByIds(string token, List<int> ids)
        {
            var response = await _studentClient.GetStudentsByIds(token, ids);
            var students = new List<StudentFullModel>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                students = JsonConvert.DeserializeObject<List<StudentFullModel>>(content);
            }

            return new ItemsResponseModel<StudentFullModel>()
            {
                StatusCode = response.StatusCode,
                Items = students
            };
        }

        public async Task<ItemResponseModel<StudentFullModel>> GetStudent(string token, int id)
        {
            var response = await _studentClient.GetStudent(token, id);
            StudentFullModel student = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<StudentFullModel>(content);
            }

            return new ItemResponseModel<StudentFullModel>()
            {
                StatusCode = response.StatusCode,
                Item = student
            };
        }

        public async Task<HttpStatusCode> CreateStudent(string token, StudentFullModel model)
        {
            var response = await _studentClient.CreateStudent(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditStudent(string token, StudentFullModel model)
        {
            var response = await _studentClient.EditStudent(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteStudent(string token, int id)
        {
            var response = await _studentClient.DeleteStudent(token, id);
            return response.StatusCode;
        }

        public async Task<ItemResponseModel<MedicalCertificateModel>> GetMedicalCertificate(string token, int id)
        {
            var response = await _studentClient.GetMedicalCertificate(token, id);
            MedicalCertificateModel certificate = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                certificate = JsonConvert.DeserializeObject<MedicalCertificateModel>(content);
            }

            return new ItemResponseModel<MedicalCertificateModel>()
            {
                StatusCode = response.StatusCode,
                Item = certificate
            };
        }

        public async Task<HttpStatusCode> CreateMedicalCertificate(string token, MedicalCertificateModel model)
        {
            var response = await _studentClient.CreateMedicalCertificate(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditMedicalCertificate(string token, MedicalCertificateModel model)
        {
            var response = await _studentClient.EditMedicalCertificate(token, model);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteMedicalCertificate(string token, int id)
        {
            var response = await _studentClient.DeleteMedicalCertificate(token, id);
            return response.StatusCode;
        }
    }
}
