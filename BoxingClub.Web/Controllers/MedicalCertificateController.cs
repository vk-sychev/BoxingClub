using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using HttpClientAdapters.Interfaces;
using HttpClients.Models;
using AuthorizeRoles = BoxingClub.Web.CustomAttributes.AuthorizeRolesAttribute;

namespace BoxingClub.Web.Controllers
{
    [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
    [Route("[controller]")]
    public class MedicalCertificateController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentClientAdapter _studentClientAdapter;


        public MedicalCertificateController(IMapper mapper,
                                            IStudentClientAdapter studentClientAdapter)
        {
            _mapper = mapper;
            _studentClientAdapter = studentClientAdapter;
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditMedicalCertificate(int id)
        {
            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.GetMedicalCertificate(token, id);

            var redirect = GetRedirectAction(response.StatusCode);
            if (redirect != null)
            {
                return redirect;
            }

            var mappedCertificate = _mapper.Map<MedicalCertificateViewModel>(response.Item);
            return View(mappedCertificate);
        }


        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> EditMedicalCertificate(MedicalCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedCertificate = _mapper.Map<MedicalCertificateModel>(model);

                var response = await _studentClientAdapter.EditMedicalCertificate(token, mappedCertificate);

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("DetailsStudent", "Student", new { id = model.StudentId });
            }
            return View(model);
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpGet("[action]")]
        public IActionResult CreateMedicalCertificate(int id)
        {
            ViewBag.studentId = id;
            return View();
        }

        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMedicalCertificate(MedicalCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["token"];
                var mappedCertificate = _mapper.Map<MedicalCertificateModel>(model);

                var response = await _studentClientAdapter.CreateMedicalCertificate(token, mappedCertificate);

                var redirect = GetRedirectAction(response);
                if (redirect != null)
                {
                    return redirect;
                }

                return RedirectToAction("DetailsStudent", "Student", new { id = model.StudentId });
            }
            return View(model);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Constants.AdminRoleName, Constants.ManagerRoleName)]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteMedicalCertificate(int id, int studentId)
        {
            var token = Request.Cookies["token"];
            var response = await _studentClientAdapter.DeleteMedicalCertificate(token, id);

            var redirect = GetRedirectAction(response);
            if (redirect != null)
            {
                return redirect;
            }

            return RedirectToAction("DetailsStudent", "Student", new { id = studentId });
        }

        private IActionResult GetRedirectAction(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                if (statusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("SignOut", "Account");
                }

                throw new InvalidOperationException("Error occurred while processing your request");
            }

            return null;
        }
    }
}
