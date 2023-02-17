using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class TicketsController : Controller
{
    private readonly IService<Ticket, TicketParameter> _service;

    public TicketsController(IService<Ticket, TicketParameter> service)
    {
        _service = service;
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
    public async Task<IActionResult> Index(TicketsViewModel ticketViewModel)
    {
        var response = await _service.GetListAsync(ticketViewModel.TicketParameter, ticketViewModel.MetaData.ToPage());
        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;
        return View(ticketViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Create(Ticket item)
    {
        await _service.CreateAsync(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Ticket item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Ticket item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }
}
