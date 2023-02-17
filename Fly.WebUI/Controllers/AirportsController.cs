using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class AirportsController : Controller
{
    private readonly IService<Airport, AirportParameter> _service;

    public AirportsController(IService<Airport, AirportParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var airportViewModel = new AirportsViewModel();
        var response = await _service.GetListAsync(new AirportParameter(), new Page());
        airportViewModel.PagedResponse = response;
        airportViewModel.MetaData = response.MetaData;
        return View(airportViewModel);
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
    public async Task<IActionResult> Detalis(int id)
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
    public async Task<IActionResult> Index(AirportsViewModel airportViewModel)
    {
        var response = await _service.GetListAsync(airportViewModel.AirportParameter, airportViewModel.MetaData.ToPage());
        airportViewModel.PagedResponse = response;
        airportViewModel.MetaData = response.MetaData;
        return View(airportViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Create(Airport item)
    {
        await _service.CreateAsync(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Airport item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Airport item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
