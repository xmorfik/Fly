using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

[Authorize]
public class SeatsController : Controller
{
    private readonly IService<Seat, SeatParameter> _service;
    private readonly IService<Aircraft, AircraftParameter> _aircrafts;
    private readonly ISeatsGeneratorService<SeatsDto> _seatsGenerator;
    private readonly ILogger<SeatsController> _logger;

    public SeatsController(
        IService<Seat, SeatParameter> service,
        IService<Aircraft, AircraftParameter> aircrafts,
        ISeatsGeneratorService<SeatsDto> seatsGenerator,
        ILogger<SeatsController> logger)
    {
        _service = service;
        _aircrafts = aircrafts;
        _seatsGenerator = seatsGenerator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var seatViewModel = new SeatsViewModel();
        var response = await _service.GetListAsync(new SeatParameter(), new Page());
        seatViewModel.PagedResponse = response;
        seatViewModel.MetaData = response.MetaData;
        return View(seatViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        int aircraftId;

        if (id != null)
        {
            aircraftId = id ?? 0;
        }
        else if (int.TryParse(Request.Cookies["SelectedAircraftId"], out aircraftId))
        {
        }
        else
        {
            return View();
        }

        try
        {
            var result = await _aircrafts.GetAsync(aircraftId);
            if (!result.Succeeded)
            {
                ViewData["SelectedAircraftId"] = null;
                ViewData["SelectedAircraft"] = null;
            }
            else
            {
                ViewData["SelectedAircraftId"] = aircraftId;
                ViewData["SelectedAircraft"] = result.Data;
            }
        }
        catch (Exception ex)
        {
            ViewData["SelectedAircraftId"] = null;
            ViewData["SelectedAircraft"] = null;
            _logger.LogError(ex.Message);
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
    public async Task<IActionResult> Index(SeatsViewModel seatViewModel)
    {
        var response = await _service.GetListAsync(seatViewModel.SeatParameter, seatViewModel.MetaData.ToPage());
        seatViewModel.PagedResponse = response;
        seatViewModel.MetaData = response.MetaData;
        return View(seatViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SeatsDto item)
    {
        Response.Cookies.Delete("SelectedAircraftId");

        await _seatsGenerator.Generate(item);
        return RedirectToAction("Index", "Aircrafts");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Seat item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Seat item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedSeatId", id.ToString());
        return Redirect(redirectUri);
    }
}
