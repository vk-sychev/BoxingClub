using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Students.API.Models;
using Students.API.WebManagers.Interfaces;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces;

namespace Students.API.WebManagers.Implementation
{
    public class StudentWebManager : IStudentWebManager
    {
        private readonly IStudentService _studentService;

        public StudentWebManager(IStudentService studentService)
        {
            _studentService = studentService;
        }


        public async Task<PageViewModel<StudentLiteDTO>> GetStudentsAsync(SearchModelDTO searchModel)
        {
            var pageModel = await _studentService.GetStudentsPaginatedAsync(searchModel);
            return new PageViewModel<StudentLiteDTO>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, pageModel.Items);
        }
    }
}
