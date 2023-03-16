using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class FlightListSpec : Specification<Flight>
{
    public FlightListSpec(FlightParameter parameter, Page? page)
    {
        Query.Include(x => x.Aircraft);

        Query.Include(x => x.DepartureAirport).ThenInclude(x => x.City);

        Query.Include(x => x.ArrivalAirport).ThenInclude(x => x.City);

        Query.Include(x => x.Tickets);

        if (parameter.FlightState is not null)
        {
            Query.Where(x => x.FlightState == parameter.FlightState);
        }

        if (parameter.DepartureDateTime is not null)
        {
            Query.Where(x => x.DepartureDateTime <= parameter.DepartureDateTime);
        }

        if (parameter.ArrivalDateTime is not null)
        {
            Query.Where(x => x.ArrivalDateTime >= parameter.ArrivalDateTime);
        }

        if (parameter.DepartureCity is not null)
        {
            Query.Where(x => x.DepartureAirport.City.Name.Contains(parameter.DepartureCity));
        }

        if (parameter.ArrivalCity is not null)
        {
            Query.Where(x => x.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));
        }

        Query.AsNoTracking();

        if (parameter.OrderBy is not null)
        {
            try
            {
                var property = typeof(Flight).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(Flight));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<Flight, object?>>(conversion, orderExpressionParam);
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

        if (page is not null)
        {
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
        }
    }
}
