using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Web.Models;
using BoxingClub.Web.WebManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.WebManagers.Implementation
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
