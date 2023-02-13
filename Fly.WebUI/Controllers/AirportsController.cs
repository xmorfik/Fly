using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    public class AirportsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Aircraft, AircraftParameter> _aircraftService;
        private readonly IService<Flight, FlightParameter> _flightService;
        private readonly IService<Airport, AirportParameter> _airportService;
        private readonly IMapper _mapper;

        public AirportsController(
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
            var result = await _airportService.GetListAsync(new AirportParameter(), new Page());
            return View(result);
        }

        public async Task<IActionResult> Detalis(int id)
        {
            var result = await _airportService.GetAsync(id);
            return View(result);
        }
    }
}
