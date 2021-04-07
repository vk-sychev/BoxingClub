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
            _mapper.Map<BoxingGroupViewModel>(await _boxingGroupService.GetBoxingGroups());
            return View();
        }
    }
}
