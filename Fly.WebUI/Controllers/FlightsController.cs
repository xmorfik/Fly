using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

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
        var flightViewModel = new FlightsViewModel();
        var response = await _service.GetListAsync(new FlightParameter(), new Page());
        flightViewModel.PagedResponse = response;
        flightViewModel.MetaData = response.MetaData;
        return View(flightViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
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
    public async Task<IActionResult> Edit(Flight item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
