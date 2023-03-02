﻿using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class TicketListSpec : Specification<Ticket>
{
    public TicketListSpec(TicketParameter parameter)
    {
        Query.Where(x => parameter.TicketState == null || x.TicketState == parameter.TicketState);

        Query.Where(x => parameter.PriceMin == null || x.Price >= parameter.PriceMin);

        Query.Where(x => parameter.PriceMax == null || x.Price <= parameter.PriceMax);

        Query.Where(x => parameter.PassengerId == null || x.PassengerId == parameter.PassengerId);

        Query.Where(x => parameter.DepartureCity == null || x.Flight.DepartureAirport.City.Name.Contains(parameter.DepartureCity));

        Query.Where(x => parameter.ArrivalCity == null || x.Flight.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));

        Query.Include(x => x.Seat);

        Query.Include(x => x.Passenger);

        Query.Include(x => x.Flight).ThenInclude(x => x.ArrivalAirport);

        Query.Include(x => x.Flight).ThenInclude(x => x.DepartureAirport);
    }
}
