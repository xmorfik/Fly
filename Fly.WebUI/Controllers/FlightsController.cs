using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class FlightsController : Controller
{
    private readonly IService<Flight, FlightParameter> _service;
    private readonly ILogger<FlightsController> _logger;

    public FlightsController(
        IService<Flight, FlightParameter> service,
        ILogger<FlightsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var flightViewModel = new FlightsViewModel();
        var parameters = new FlightParameter();
        if (isSelect)
        {
            parameters.FlightState = FlightState.Scheduled;
        }

        var response = await _service.GetListAsync(parameters, new Page());

        flightViewModel.PagedResponse = response;
        flightViewModel.MetaData = response.MetaData;
        flightViewModel.IsSelect = isSelect;
        flightViewModel.RedirectUri = redirectUri;

        return View(flightViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        int aircraftId;
        int airportId;

        if (int.TryParse(Request.Cookies["SelectedAircraftId"], out aircraftId))
        {
            ViewData["SelectedAircraftId"] = aircraftId;
        }

        if (int.TryParse(Request.Cookies["SelectedAirportId"], out airportId))
        {
            ViewData["SelectedAirportId"] = airportId;
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Index(FlightsViewModel flightViewModel)
    {
        var response = await _service.GetListAsync(flightViewModel.FlightParameter, flightViewModel.MetaData.ToPage());
        flightViewModel.PagedResponse = response;
        flightViewModel.MetaData = response.MetaData;
        return View(flightViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Flight item)
    {
        Response.Cookies.Delete("SelectedAircraftId");
        Response.Cookies.Delete("SelectedAirportId");

        await _service.CreateAsync(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Flight item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Flight item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedFlightId", id.ToString());
        return Redirect(redirectUri);
    }
}
