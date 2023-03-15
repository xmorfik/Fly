using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class TicketListSpec : Specification<Ticket>
{
    public TicketListSpec(TicketParameter parameter, Page? page)
    {
       
        Query.Include(x => x.Seat);

        Query.Include(x => x.Passenger);

        Query.Include(x => x.Flight).ThenInclude(x => x.ArrivalAirport);

        Query.Include(x => x.Flight).ThenInclude(x => x.DepartureAirport);

        if(parameter.TicketState is not null)
        {
            Query.Where(x => x.TicketState == parameter.TicketState);
        }

        if (parameter.PriceMin is not null)
        {
            Query.Where(x => x.Price >= parameter.PriceMin);
        }

        if (parameter.PriceMax is not null)
        {
            Query.Where(x => x.Price <= parameter.PriceMax);

        }

        if (parameter.PassengerId is not null)
        {
            Query.Where(x => x.PassengerId == parameter.PassengerId);
        }

        if (parameter.DepartureCity is not null)
        {
            Query.Where(x =>  x.Flight.DepartureAirport.City.Name.Contains(parameter.DepartureCity));
        }

        if (parameter.ArrivalCity is not null)
        {
            Query.Where(x => x.Flight.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));
        }

        if (parameter.OrderBy is not null)
        {
            try
            {
                var property = typeof(Ticket).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(Ticket));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<Ticket, object?>>(conversion, orderExpressionParam);
                if (parameter.Descresing)
                {
                    Query.OrderByDescending(orderLambda);
                }
                else
                {
                    Query.OrderBy(orderLambda);
                }
            }
            catch (Exception ex)
            {
                Query.OrderByDescending(x => x.Id);
            }
        }
        else
        {
            Query.OrderByDescending(x => x.Id);
        }

        if (page != null)
        {
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
        }
    }
}
