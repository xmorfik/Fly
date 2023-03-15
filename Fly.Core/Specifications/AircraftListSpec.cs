using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class AircraftListSpec : Specification<Aircraft>
{
    public AircraftListSpec(AircraftParameter parameter, Page? page)
    {
        Query.Include(x => x.Airline);

        Query.Include(x => x.Airport).ThenInclude(x => x.City);

        Query.Include(x => x.Seats);

        if(parameter.AircraftState is not null)
        {
            Query.Where(x => x.AircraftState == parameter.AircraftState);
        }

        if (parameter.Airline is not null)
        {
            Query.Where(x => x.Airline.Name.Contains(parameter.Airline));
        }

        if (parameter.ModelType is not null)
        {
            Query.Where(x => x.ModelType.Contains(parameter.ModelType));
        }

        if (parameter.AirlineId is not null)
        {
            Query.Where(x => x.Airline.Id == parameter.AirlineId);
        }

        if (parameter.OrderBy is not null)
        {
            try
            {
                var property = typeof(Aircraft).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(Aircraft));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<Aircraft, object?>>(conversion, orderExpressionParam);
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
