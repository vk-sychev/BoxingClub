using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Models;
using HttpClients.Models.SpecModels;

namespace HttpClients.Interfaces
{
    public interface IStudentClient
    {
        Task<HttpResponseMessage> GetBoxingGroups(string token, SearchModel searchModel);

        Task<HttpResponseMessage> GetBoxingGroups(string token);

        Task<HttpResponseMessage> GetBoxingGroup(string token, int id);

        Task<HttpResponseMessage> GetBoxingGroupWithStudents(string token, int id);

        Task<HttpResponseMessage> DeleteBoxingGroup(string token, int id);

        Task<HttpResponseMessage> EditBoxingGroup(string token, BoxingGroupLiteModel model);

        Task<HttpResponseMessage> CreateBoxingGroup(string token, BoxingGroupLiteModel model);

        Task<HttpResponseMessage> DeleteStudentFromBoxingGroup(string token, int studentId);


        Task<HttpResponseMessage> GetStudents(string token, SearchModel searchModel);

        Task<HttpResponseMessage> GetStudent(string token, int id);

        Task<HttpResponseMessage> CreateStudent(string token, StudentFullModel model);

        Task<HttpResponseMessage> EditStudent(string token, StudentFullModel model);

        Task<HttpResponseMessage> DeleteStudent(string token, int id);

        Task<HttpResponseMessage> GetStudentsBySpecification(string token, TournamentWithSpecification tournamentWithSpecification);

        Task<HttpResponseMessage> GetStudentsByIds(string token, List<int> ids);


        Task<HttpResponseMessage> GetMedicalCertificate(string token, int id);

        Task<HttpResponseMessage> CreateMedicalCertificate(string token, MedicalCertificateModel model);

        Task<HttpResponseMessage> EditMedicalCertificate(string token, MedicalCertificateModel model);

        Task<HttpResponseMessage> DeleteMedicalCertificate(string token, int id);
    }
}
