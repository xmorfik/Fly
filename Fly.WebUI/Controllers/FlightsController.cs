using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IService<Flight, FlightParameter> _service;

        public FlightsController(IService<Flight, FlightParameter> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new FlightsViewModel();
            var response = await _service.GetListAsync(new FlightParameter(), new Page());
            vm.MetaData = response.MetaData;
            ViewData["flights"] = response;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FlightsViewModel flightViewModel)
        {
            var response = await _service.GetListAsync(flightViewModel.FlightParameter, flightViewModel.MetaData.ToPage());
            ViewData["flights"] = response;
            return View(flightViewModel);
        }
    }
}
