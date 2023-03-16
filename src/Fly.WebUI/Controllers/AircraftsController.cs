using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

[Authorize]
public class AircraftsController : Controller
{
    private readonly IService<Aircraft, AircraftParameter> _service;
    private readonly IService<Airport, AirportParameter> _airports;

    private readonly IMapper _mapper;
    public AircraftsController(
        IService<Aircraft, AircraftParameter> service,
        IService<Airport, AirportParameter> airports,
        IMapper mapper)
    {
        _service = service;
        _airports = airports;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var aircraftViewModel = new AircraftsViewModel();
        var aircraftParameter = new AircraftParameter();

        var response = await _service.GetListAsync(aircraftParameter, new Page());

        aircraftViewModel.PagedResponse = response;
        aircraftViewModel.MetaData = response.MetaData;
        aircraftViewModel.IsSelect = isSelect;
        aircraftViewModel.RedirectUri = redirectUri;

        return View(aircraftViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(AircraftsViewModel aircraftViewModel)
    {
        var response = await _service.GetListAsync(aircraftViewModel.AircraftParameter, aircraftViewModel.MetaData.ToPage());

        aircraftViewModel.PagedResponse = response;
        aircraftViewModel.MetaData = response.MetaData;

        return View(aircraftViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        int airportId;
        int airlineId;

        if (int.TryParse(Request.Cookies["SelectedAirportId"], out airportId))
        {
            ViewData["SelectedAirportId"] = airportId;
            var result = await _airports.GetAsync(airportId);
            ViewData["SelectedAirport"] = result.Data;
        }

        if (int.TryParse(Request.Cookies["SelectedAirlineId"], out airlineId))
        {
            ViewData["SelectedAirlineId"] = airlineId;
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AircarftForCreationDto item)
    {
        Response.Cookies.Delete("SelectedAirlineId");
        Response.Cookies.Delete("SelectedAirportId");

        var result = _mapper.Map<Aircraft>(item);
        await _service.CreateAsync(result);

        return RedirectToAction("index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Aircraft item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Aircraft item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedAircraftId", id.ToString());
        return Redirect(redirectUri);
    }
}
