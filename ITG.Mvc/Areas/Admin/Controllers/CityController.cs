using ITG.Services.Abstract;
using ITG.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async  Task<IActionResult> Index()
        {
            var result = await _cityService.GetAll();
            if(result.ResultStatus==ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
