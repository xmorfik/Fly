using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class TicketsController : Controller
{
    private readonly IService<Ticket, TicketParameter> _service;
    private readonly IService<Flight, FlightParameter> _flights;
    private readonly ILogger<TicketsController> _logger;
    private readonly ITicketsGeneratorService<TicketsDto> _ticketsGenerator;

    public TicketsController(
        IService<Ticket, TicketParameter> service,
        IService<Flight, FlightParameter> flights,
        ILogger<TicketsController> logger,
        ITicketsGeneratorService<TicketsDto> ticketsGenerator)
    {
        _service = service;
        _flights = flights;
        _logger = logger;
        _ticketsGenerator = ticketsGenerator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var ticketViewModel = new TicketsViewModel();
        var response = await _service.GetListAsync(new TicketParameter(), new Page());
        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;
        ticketViewModel.IsSelect = isSelect;
        ticketViewModel.RedirectUri = redirectUri;

        return View(ticketViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        int flightId;

        if (id != null)
        {
            flightId = id ?? 0;
        }
        else if (int.TryParse(Request.Cookies["SelectedAircraftId"], out flightId))
        {
        }
        else
        {
            return View();
        }

        try
        {
            var result = await _flights.GetAsync(flightId);
            if (!result.Succeeded)
            {
                ViewData["SelectedFlightId"] = null;
                ViewData["SelectedFlight"] = null;
            }
            else
            {
                ViewData["SelectedFlightId"] = flightId;
                ViewData["SelectedFlight"] = result.Data;
            }
        }
        catch (Exception ex)
        {
            ViewData["SelectedFlightId"] = null;
            ViewData["SelectedFlight"] = null;
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
        Response.Cookies.Delete("SelectedFlightId");
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

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedTicketId", id.ToString());
        return Redirect(redirectUri);
    }
}
