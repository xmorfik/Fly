using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

[Authorize(Policy = "AdministratorOnly")]
public class AirlinesController : Controller
{
    private readonly IService<Airline, AirlineParameter> _service;

    public AirlinesController(IService<Airline, AirlineParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var airlineViewModel = new AirlinesViewModel();
        var response = await _service.GetListAsync(new AirlineParameter(), new Page());
        airlineViewModel.PagedResponse = response;
        airlineViewModel.MetaData = response.MetaData;
        return View(airlineViewModel);
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
    public async Task<IActionResult> Index(AirlinesViewModel airlineViewModel)
    {
        var response = await _service.GetListAsync(airlineViewModel.AirlineParameter, airlineViewModel.PagedResponse.MetaData.ToPage());
        airlineViewModel.PagedResponse = response;
        airlineViewModel.MetaData = response.MetaData;
        return View(airlineViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Create(Airline item)
    {
        await _service.CreateAsync(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Airline item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Airline item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
