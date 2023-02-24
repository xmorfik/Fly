using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Models;

namespace Fly.WebUI.Services
{
    public class TicketsGeneratorService : ITicketsGeneratorService<TicketsDto>
    {
        private readonly IService<Flight, FlightParameter> _flights;
        private readonly IService<Aircraft, AircraftParameter> _aircarfts;
        private readonly IService<Ticket, TicketParameter> _tickets;
        public TicketsGeneratorService(
            IService<Aircraft, AircraftParameter> aircarfts,
            IService<Flight, FlightParameter> flights,
            IService<Ticket,TicketParameter > tikets)
        {
            _flights = flights;
            _tickets = tikets;
            _aircarfts = aircarfts;
        }

        public async Task Generate(TicketsDto ticketsDto)
        {
            var flight = await _flights.GetAsync(ticketsDto.FlightId ?? 0);
            var aircarft = await _aircarfts.GetAsync(flight.Data.AircraftId ?? 0);

            foreach(var seat in aircarft.Data.Seats)
            {
                var ticket = new Ticket()
                {
                    FlightId = ticketsDto.FlightId,
                    SeatId = seat.Id
                };

                if(seat.SeatClass == SeatClass.EconomClass)
                {
                    ticket.Price = ticketsDto.EconomClassPrice;
                }
                else if(seat.SeatClass == SeatClass.FirstClass)
                {
                    ticket.Price = ticketsDto.FirstClassPrice;
                }
                else if(seat.SeatClass == SeatClass.BusinessClass)
                {
                    ticket.Price = ticketsDto.BusinessClassPrice;
                }
                await _tickets.CreateAsync(ticket);
            }
        }
    }
}
