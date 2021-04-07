using AutoMapper;
using BoxingClub.BLL.Interfaces;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoxingGroupService _boxingGroupService;

        public HomeController(IMapper mapper, 
                              IBoxingGroupService boxingGroupService)
        {
            _mapper = mapper;
            _boxingGroupService = boxingGroupService;
        }

        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<List<BoxingGroupViewModel>>(await _boxingGroupService.GetBoxingGroups());
            return View(model);
        }

        [HttpGet]
        [Route("EditGroup/{id}")]
        public async Task<IActionResult> EditGroup(int? id)
        {
            var group = await _boxingGroupService.GetBoxingGroup(id);
            var mappedGroup = _mapper.Map<BoxingGroupViewModel>(group);
            return View(mappedGroup);
        }

        [HttpPost]
        [Route("EditGroup/{id}")]

        public async Task<IActionResult> EditGroup(BoxingGroupViewModel model)
        {

        }
    }
}
