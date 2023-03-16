using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Interfaces;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class TicketsStateService : ITicketsStateService
{
    private readonly IRepository<Flight> _flightsRepository;
    private readonly IRepository<Ticket> _ticketsRepository;

    public TicketsStateService(
        IRepository<Flight> flightsRepository,
        IRepository<Ticket> ticketsRepository)
    {
        _flightsRepository = flightsRepository;
        _ticketsRepository = ticketsRepository;
    }

    public async Task SetTicketsStateOnStartFlight(int id)
    {
        var flight = await _flightsRepository.FirstOrDefaultAsync(new FlightSpec(id));

        foreach (var ticket in flight.Tickets)
        {
            var currentTicketState = ticket.TicketState;
            var result = currentTicketState switch
            {
                TicketState.Active => TicketState.Unused,
                TicketState.Sold => TicketState.Used,
                _ => currentTicketState
            };
            ticket.TicketState = result;

            await _ticketsRepository.UpdateAsync(ticket);
        }
    }
}
