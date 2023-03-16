using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class AirportListSpec : Specification<Airport>
{
    public AirportListSpec(AirportParameter parameter, Page? page)
    {
        Query.Include(x => x.City);

        if (parameter.CityName is not null)
        {
            Query.Where(x => x.City.Name.Contains(parameter.CityName));
        }

        if (parameter.IsoRegion is not null)
        {
            Query.Where(x => x.City.IsoRegion.Contains(parameter.IsoRegion));
        }

        if (parameter.IsoCountry is not null)
        {
            Query.Where(x => x.City.IsoCountry.Contains(parameter.IsoCountry));
        }

        if (parameter.Name is not null)
        {
            Query.Where(x => x.Name.Contains(parameter.Name));
        }

        if (parameter.OrderBy is not null)
        {
            try
            {
                var property = typeof(Airport).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(Airport));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<Airport, object?>>(conversion, orderExpressionParam);
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
