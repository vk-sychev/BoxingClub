using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.CustomAttributes;
using BoxingClub.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using Students.API.Models;
using Students.API.WebManagers.Interfaces;
using Students.BLL.DomainEntities;
using Students.BLL.DomainEntities.SpecModels;
using Students.BLL.Interfaces;

namespace Students.API.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName, Constants.CoachRoleName)]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IBoxingGroupService _boxingGroupService;
        private readonly IStudentSelectionService _studentSelectionService;
        private readonly IMedicalCertificateService _medicalCertificateService;
        private readonly IStudentWebManager _studentWebManager;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService,
                                 IMapper mapper,
                                 IBoxingGroupService boxingGroupService,
                                 IStudentSelectionService studentSelectionService,
                                 IMedicalCertificateService medicalCertificateService,
                                 IStudentWebManager studentWebManager)
        {
            _studentService = studentService;
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
            _studentSelectionService = studentSelectionService;
            _medicalCertificateService = medicalCertificateService;
            _studentWebManager = studentWebManager;
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents(SearchModelDTO searchModel)
        {
            var pageViewModel = await _studentWebManager.GetStudentsAsync(searchModel);
            return Ok(pageViewModel);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentsBySpecification([FromBody]TournamentWithSpecificationDTO tournamentWithSpecification)
        {
            var students = await _studentSelectionService.GetStudentsBySpecification(tournamentWithSpecification.Tournament, tournamentWithSpecification.TournamentSpecification);
            return Ok(students);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentsByIds(List<int> ids)
        {
            var students = await _studentSelectionService.GetStudentsByIds(ids);
            return Ok(students);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent([FromBody]StudentFullDTO student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateStudentAsync(student);
                return Ok();
            }

            return BadRequest();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Ok();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var studentDTO = await _studentService.GetStudentByIdAsync(id);
            return Ok(studentDTO);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditStudent([FromBody]StudentFullDTO student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(student);
                return Ok();
            }

            return BadRequest();
        }
    }
}
