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

        Query.Where(x => parameter.FlightState == null || x.FlightState == parameter.FlightState);

        Query.Where(x => parameter.DepartureDateTime == null || x.DepartureDateTime <= parameter.DepartureDateTime);

        Query.Where(x => parameter.ArrivalDateTime == null || x.ArrivalDateTime >= parameter.ArrivalDateTime);

        Query.Where(x => parameter.DepartureCity == null || x.DepartureAirport.City.Name.Contains(parameter.DepartureCity));

        Query.Where(x => parameter.ArrivalCity == null || x.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));

        if (page != null)
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
            catch
            {
                Query.OrderByDescending(x => x.Id);
            }
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
        }
    }
}
