using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class AircraftsController : Controller
{
    private readonly IService<Aircraft, AircraftParameter> _service;
    private readonly IMapper _mapper;
    public AircraftsController(IService<Aircraft, AircraftParameter> service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var aircraftViewModel = new AircraftsViewModel();
        var response = await _service.GetListAsync(new AircraftParameter(), new Page());
        aircraftViewModel.PagedResponse = response;
        aircraftViewModel.MetaData = response.MetaData;
        return View(aircraftViewModel);
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
    public async Task<IActionResult> Index(AircraftsViewModel aircraftViewModel)
    {
        var response = await _service.GetListAsync(aircraftViewModel.AircraftParameter, aircraftViewModel.MetaData.ToPage());
        aircraftViewModel.PagedResponse = response;
        aircraftViewModel.MetaData = response.MetaData;
        return View(aircraftViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateAircraftDto item)
    {
        var result = _mapper.Map<Aircraft>(item);
        await _service.CreateAsync(result);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Aircraft item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Aircraft item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
