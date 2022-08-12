using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Students.API.Models;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces;

namespace Students.API.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
    [Route("[controller]")]
    public class MedicalCertificateController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicalCertificateService _medicalCertificateService;


        public MedicalCertificateController(IMapper mapper,
                                            IMedicalCertificateService medicalCertificateService)
        {
            _mapper = mapper;
            _medicalCertificateService = medicalCertificateService;
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetMedicalCertificate(int id)
        {
            var medicalCertificate = await _medicalCertificateService.GetMedicalCertificateByIdAsync(id);
            return Ok(medicalCertificate);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> EditMedicalCertificate(MedicalCertificateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _medicalCertificateService.UpdateMedicalCertificateAsync(model);
                return Ok();
            }

            return BadRequest();
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMedicalCertificate(MedicalCertificateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _medicalCertificateService.CreateMedicalCertificateAsync(model);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("[action]/{id}")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        public async Task<IActionResult> DeleteMedicalCertificate(int id)
        {
            await _medicalCertificateService.DeleteMedicalCertificateAsync(id);
            return Ok();
        }
    }
}
