using System.Threading.Tasks;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.WebManagers.Interfaces
{
    public interface IStudentWebManager
    {
        Task<PageViewModel<StudentLiteDTO>> GetStudentsAsync(SearchModelDTO searchModel);
    }
}