using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class SeatsController : Controller
{
    private readonly IService<Seat, SeatParameter> _service;
    private readonly ISeatsGeneratorService<SeatsDto> _seatsGenerator;
    public SeatsController(IService<Seat, SeatParameter> service,
        ISeatsGeneratorService<SeatsDto> seatsGenerator)
    {
        _service = service;
        _seatsGenerator = seatsGenerator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var seatViewModel = new SeatsViewModel();
        var response = await _service.GetListAsync(new SeatParameter(), new Page());
        seatViewModel.PagedResponse = response;
        seatViewModel.MetaData = response.MetaData;
        return View(seatViewModel);
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
    public async Task<IActionResult> Index(SeatsViewModel seatViewModel)
    {
        var response = await _service.GetListAsync(seatViewModel.SeatParameter, seatViewModel.MetaData.ToPage());
        seatViewModel.PagedResponse = response;
        seatViewModel.MetaData = response.MetaData;
        return View(seatViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Create(SeatsDto item)
    {
        await _seatsGenerator.Generate(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Seat item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Seat item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
