using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fly.WebUI.Controllers;

[Authorize(Roles = "Passenger")]
public class PassengerController : Controller
{
    public readonly IService<Passenger, PassengerParameter> _service;
    public readonly IService<Ticket, TicketParameter> _tickets;
    public readonly Passenger _passenger;

    public PassengerController(
        IService<Passenger, PassengerParameter> service,
        IService<Ticket, TicketParameter> tickets)
    {
        _service = service;
        _tickets = tickets;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue("sub");
        var user = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
        return View(user.FirstOrDefault());
    }

    [HttpPost]
    public async Task<IActionResult> Profile(Passenger passenger)
    {
        await _service.UpdateAsync(passenger);
        return Redirect("/passenger");
    }

    [HttpGet]
    public async Task<IActionResult> Tickets()
    {
        var ticketViewModel = new TicketsViewModel();
        var userId = User.FindFirstValue("sub");
        var user = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
        var response = await _tickets.GetListAsync(new TicketParameter { PassengerId = user.FirstOrDefault().Id ?? 0 }, new Page());
        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;
        return View(ticketViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Tickets(TicketsViewModel ticketViewModel)
    {
        var userId = User.FindFirstValue("sub");
        var user = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
        ticketViewModel.TicketParameter.PassengerId = user.FirstOrDefault().Id ?? 0;
        var response = await _tickets.GetListAsync(ticketViewModel.TicketParameter, ticketViewModel.MetaData.ToPage());
        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;
        return View(ticketViewModel);
    }
}
