using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.CustomAttributes;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
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
        [Route("MedicalCertificate/EditMedicalCertificate/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditMedicalCertificate(int? id)
        {
            var medicalCertificateDTO = await _medicalCertificateService.GetMedicalCertificateByIdAsync(id);
            var medicalCertificate = _mapper.Map<MedicalCertificateViewModel>(medicalCertificateDTO);
            return View(medicalCertificate);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("MedicalCertificate/EditMedicalCertificate/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditMedicalCertificate(MedicalCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medicalCertificateDTO = _mapper.Map<MedicalCertificateDTO>(model);
                await _medicalCertificateService.UpdateMedicalCertificateAsync(medicalCertificateDTO);
                return RedirectToAction("DetailsStudent", "Student", new { id = model.StudentId });
            }
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("MedicalCertificate/CreateMedicalCertificate")]
        [HttpGet]
        public IActionResult CreateMedicalCertificate(int? id)
        {
            ViewBag.studentId = id;
            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("MedicalCertificate/CreateMedicalCertificate")]
        [HttpPost]
        public async Task<IActionResult> CreateMedicalCertificate(MedicalCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medicalCertificateDTO = _mapper.Map<MedicalCertificateDTO>(model);
                await _medicalCertificateService.CreateMedicalCertificateAsync(medicalCertificateDTO);
                return RedirectToAction("DetailsStudent", "Student", new { id = model.StudentId });
            }
            return View(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("MedicalCertificate/DeleteMedicalCertificate/{id}")]
        public async Task<IActionResult> DeleteMedicalCertificate(int? id, int? studentId)
        {
            await _medicalCertificateService.DeleteMedicalCertificateAsync(id);
            return RedirectToAction("DetailsStudent", "Student", new { id = studentId });
        }
    }
}
