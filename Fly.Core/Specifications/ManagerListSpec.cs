using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class ManagerListSpec : Specification<Manager>
{
    public ManagerListSpec(ManagerParameter parameter, Page? page)
    {
        Query.Where(x => parameter.AirlineName == null || x.Airline.Name.Contains(parameter.AirlineName));

        Query.Where(x => parameter.UserName == null || x.User.UserName.Contains(parameter.UserName));

        Query.Include(x => x.User);

        Query.Include(x => x.Airline);

        if (page != null)
        {
            try
            {
                var property = typeof(Manager).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(Manager));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<Manager, object?>>(conversion, orderExpressionParam);
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
