using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fly.WebUI.Controllers;

[Authorize(Roles = "Passenger")]
public class PassengerController : Controller
{
    public readonly IService<Passenger, PassengerParameter> _service;
    public readonly IService<Ticket, TicketParameter> _tickets;
    public readonly IConfiguration _configuration;
    public readonly ILogger<PassengerController> _logger;

    public PassengerController(
        IService<Passenger, PassengerParameter> service,
        IService<Ticket, TicketParameter> tickets,
        IConfiguration configuration,
        ILogger<PassengerController> logger)
    {
        _service = service;
        _tickets = tickets;
        _configuration = configuration;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue("sub");
        if (userId == null)
        {
            return NotFound();
        }

        var user = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
        if (user.FirstOrDefault() == null)
        {
            var passenger = new Passenger { UserId = userId };
            return View(passenger);
        }

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
        var passengers = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
        var passenger = new Passenger();

        try
        {
            passenger = passengers?.FirstOrDefault();
            if (passenger is null)
            {
                _logger.LogCritical($"Cant find passanger {userId}");
                return RedirectToAction("Error", "Home");
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return RedirectToAction("Error", "Home");
        }
        
        var response = await _tickets.GetListAsync(new TicketParameter { PassengerId = passenger.Id }, new Page());

        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;

        return View(ticketViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Tickets(TicketsViewModel ticketViewModel)
    {
        var userId = User.FindFirstValue("sub");
        var passengers = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
        var passenger = new Passenger();

        try
        {
            passenger = passengers?.FirstOrDefault();
            if (passenger is null)
            {
                _logger.LogCritical($"Cant find passanger {userId}");
                return RedirectToAction("Error", "Home");
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return RedirectToAction("Error", "Home");
        }

        ticketViewModel.TicketParameter.PassengerId = passenger.Id;
        var response = await _tickets.GetListAsync(ticketViewModel.TicketParameter, ticketViewModel.MetaData.ToPage());

        ticketViewModel.PagedResponse = response;
        ticketViewModel.MetaData = response.MetaData;

        return View(ticketViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Payoff(int id)
    {
        var payoffVm = new PayoffViewModel();
        try
        {
            var userId = User.FindFirstValue("sub");
            var user = await _service.GetListAsync(new PassengerParameter { UserId = userId }, new Page());
            var passenger = user?.FirstOrDefault();
            if (passenger == null)
            {
                _logger.LogCritical($"{userId} not created as passenger");
                return RedirectToAction("Error", "Home");
            }

            var result = await _tickets.GetAsync(id);
            if (!result.Succeeded || result.Data == null)
            {
                _logger.LogCritical($"Ticket {id} does not exist");
                return RedirectToAction("Error", "Home");
            }

            var ticket = result.Data;

            var ticketId = ticket.Id ?? 0;
            var passengerId = passenger.Id ?? 0;

            if (passengerId == 0)
            {
                _logger.LogCritical($"Null id passenger field, userId {userId}");
                return RedirectToAction("Error", "Home");
            }

            if (ticketId == 0)
            {
                _logger.LogCritical($"Null id ticket field, userId {userId}");
                return RedirectToAction("Error", "Home");
            }

            payoffVm.PassengerId = passengerId;
            payoffVm.TicketId = ticketId;
            payoffVm.Amount = ticket.Price;
            payoffVm.Description = "Ticket payment";

            var request = new LiqPayRequest
            {
                Version = 3,
                PublicKey = _configuration["LiqPayPublicKey"],
                Action = LiqPayRequestAction.Pay,
                Amount = (double)payoffVm.Amount,
                Currency = "UAH",
                Description = payoffVm.Description,
                OrderId = Guid.NewGuid().ToString(),
                //CardToken = "sandbox_token"
            };

            var liqPayClient = new LiqPayClient(_configuration["LiqPayPublicKey"], _configuration["LiqPayPrivateKey"]);
            liqPayClient.IsCnbSandbox = true;
            var res = liqPayClient.GenerateDataAndSignature(request);

            ViewData["Data"] = res.Key;
            ViewData["Signature"] = res.Value;
        }
        catch
        {
            _logger.LogCritical($"Payoff crashed on get");
        }

        return View(payoffVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Payoff(PayoffViewModel payoffViewModel)
    {
        if (payoffViewModel.IsSuccessed)
        {
            var result = await _tickets.GetAsync(payoffViewModel.TicketId);
            if (result.Succeeded)
            {
                var ticket = result.Data;
                ticket.PassengerId = payoffViewModel.PassengerId;
                ticket.SoldDate = DateTime.Now;
                ticket.TicketState = TicketState.Sold;
                ticket.Seat = null;
                ticket.Flight = null;
                await _tickets.UpdateAsync(ticket);
                return RedirectToAction("Tickets");
            }
            _logger.LogCritical($"Cant get ticket {payoffViewModel.TicketId}");
        }
        _logger.LogCritical($"Payoff crashed on post");
        return RedirectToAction("Error", "Home");
    }
}
