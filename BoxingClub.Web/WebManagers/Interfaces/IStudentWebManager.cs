using BoxingClub.BLL.DomainEntities;
using BoxingClub.Web.Models;
using System.Threading.Tasks;

namespace BoxingClub.Web.WebManagers.Interfaces
{
    public interface IStudentWebManager
    {
        Task<PageViewModel<StudentLiteViewModel>> GetStudentsAsync(SearchModelDTO searchModel);
    }
}