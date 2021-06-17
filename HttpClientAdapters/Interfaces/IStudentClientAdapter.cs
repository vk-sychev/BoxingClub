using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpClientAdapters.Models;
using HttpClients.Models;

namespace HttpClientAdapters.Interfaces
{
    public interface IStudentClientAdapter
    {
        Task<PageModelResponse<BoxingGroupLiteModel>> GetBoxingGroups(string token, SearchModel searchModel);

        Task<ItemsResponseModel<BoxingGroupLiteModel>> GetBoxingGroups(string token);

        Task<ItemResponseModel<BoxingGroupLiteModel>> GetBoxingGroup(string token, int id);

        Task<ItemResponseModel<BoxingGroupFullModel>> GetBoxingGroupWithStudents(string token, int id);

        Task<HttpStatusCode> DeleteBoxingGroup(string token, int id);

        Task<HttpStatusCode> EditBoxingGroup(string token, BoxingGroupLiteModel model);

        Task<HttpStatusCode> CreateBoxingGroup(string token, BoxingGroupLiteModel model);

        Task<HttpStatusCode> DeleteStudentFromBoxingGroup(string token, int studentId);


        Task<PageModelResponse<StudentLiteModel>> GetStudents(string token, SearchModel searchModel);

        Task<ItemResponseModel<StudentFullModel>> GetStudent(string token, int id);

        Task<HttpStatusCode> CreateStudent(string token, StudentFullModel model);

        Task<HttpStatusCode> EditStudent(string token, StudentFullModel model);

        Task<HttpStatusCode> DeleteStudent(string token, int id);
    }
}
