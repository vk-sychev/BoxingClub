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
        private readonly IMapper _mapper;

        public StudentWebManager(IStudentService studentService,
                                 IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }


        public async Task<PageViewModel<StudentLiteViewModel>> GetStudentsAsync(SearchModelDTO searchModel)
        {
            var pageModel = await _studentService.GetStudentsPaginatedAsync(searchModel);
            var students = _mapper.Map<List<StudentLiteViewModel>>(pageModel.Items);
            return new PageViewModel<StudentLiteViewModel>(pageModel.Count, searchModel.PageIndex, searchModel.PageSize, students);
        }
    }
}
