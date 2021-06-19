using System.Collections.Generic;
using System.ComponentModel;
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
            var mappedStudents = _mapper.Map<List<StudentFullViewModel>>(students);
            return Ok(mappedStudents);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentsByIds(List<int> ids)
        {
            var students = await _studentSelectionService.GetStudentsByIds(ids);
            var mappedStudents = _mapper.Map<List<StudentFullViewModel>>(students);
            return Ok(mappedStudents);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(StudentFullViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.CreateStudentAsync(studentDTO);
                return Ok();
            }

            return BadRequest();
        }

        [AuthorizeRoles(Constants.AdminRoleName)]
        //[Route("[action]/{id}")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Ok();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetStudent(int id, bool fromHomeController, int returnId)
        {
            var studentDTO = await _studentService.GetStudentByIdAsync(id);
            var student = _mapper.Map<StudentFullViewModel>(studentDTO);
            return Ok(student);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> EditStudent(StudentFullViewModel studentViewModel, bool fromHomeController, int returnId)
        {
            if (ModelState.IsValid)
            {
                var studentDTO = _mapper.Map<StudentFullDTO>(studentViewModel);
                await _studentService.UpdateStudentAsync(studentDTO);
                return Ok();
            }

            return BadRequest();
        }
    }
}
