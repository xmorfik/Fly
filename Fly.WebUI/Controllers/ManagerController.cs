using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Aircraft, AircraftParameter> _aircraftService;
        private readonly IService<Flight, FlightParameter> _flightService;
        private readonly IService<Airport, AirportParameter> _airportService;
        private readonly IMapper _mapper;
        private readonly Manager _manager;

        public ManagerController(
            ILogger<HomeController> logger,
            IService<Aircraft, AircraftParameter> aircraftService,
            IService<Flight, FlightParameter> flightService,
            IService<Airport, AirportParameter> airportService,
            IMapper mapper)
        {
            _logger = logger;
            _aircraftService = aircraftService;
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FlightCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FlightCreate([Bind] FlightCreateViewModel model)
        {
            var result = _mapper.Map<Flight>(model);
            await _flightService.CreateAsync(result);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AircraftCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AircraftCreate([Bind] AircraftCreateViewModel model)
        {
            var result = _mapper.Map<Aircraft>(model);
            await _aircraftService.CreateAsync(result);
            return View();
        }
    }
}
