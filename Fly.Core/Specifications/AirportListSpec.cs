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

        Query.Where(x => parameter.CityName == null || x.City.Name.Contains(parameter.CityName));

        Query.Where(x => parameter.IsoRegion == null || x.City.IsoRegion.Contains(parameter.IsoRegion));

        Query.Where(x => parameter.IsoCountry == null || x.City.IsoCountry.Contains(parameter.IsoCountry));

        Query.Where(x => parameter.Name == null || x.Name.Contains(parameter.Name));

        if(page != null)
        {
            if (parameter.OrderBy != null)
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
                catch(Exception ex)
                {
                    Query.OrderByDescending(x => x.Id);
                }
            }
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
        }
    }
}
