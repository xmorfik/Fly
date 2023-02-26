using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;
using Fly.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class TicketsController : Controller
{
    private readonly IService<Ticket, TicketParameter> _service;
    private readonly ITicketsGeneratorService<TicketsDto> _ticketsGenerator;
    public TicketsController(IService<Ticket, TicketParameter> service,
        ITicketsGeneratorService<TicketsDto> ticketsGenerator )
    {
        _service = service;
        _ticketsGenerator = ticketsGenerator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var ticketViewModel = new TicketsViewModel();
        var response = await _service.GetListAsync(new TicketParameter(), new Page());
        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;
        return View(ticketViewModel);
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
    public async Task<IActionResult> Index(TicketsViewModel ticketViewModel)
    {
        var response = await _service.GetListAsync(ticketViewModel.TicketParameter, ticketViewModel.MetaData.ToPage());
        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;
        return View(ticketViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TicketsDto item)
    {
        await _ticketsGenerator.Generate(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Ticket item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Ticket item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
