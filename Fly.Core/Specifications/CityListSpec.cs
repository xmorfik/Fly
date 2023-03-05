using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class CityListSpec : Specification<City>
{
    public CityListSpec(CityParameter parameter, Page? page)
    {
        Query.Where(x => parameter.Name == null || x.Name.Contains(parameter.Name));

        if (page != null)
        {
            try
            {
                var property = typeof(City).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(City));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<City, object?>>(conversion, orderExpressionParam);
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
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).OrderByDescending(x => x.Id);
        }
    }
}
