using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    public class AircraftsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Aircraft, AircraftParameter> _aircraftService;
        private readonly IService<Flight, FlightParameter> _flightService;
        private readonly IService<Airport, AirportParameter> _airportService;
        private readonly IMapper _mapper;

        public AircraftsController(
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
        public async Task<IActionResult> Index()
        {
            var result = await _aircraftService.GetListAsync(new AircraftParameter(), new Page());
            return View(result.Data);
        }

        public async Task<IActionResult> Detalis(int id)
        {
            var result = await _aircraftService.GetAsync(id);
            return View(result.Data);
        }
    }
}
