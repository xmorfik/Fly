using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class SeatListSpec : Specification<Seat>
{
    public SeatListSpec(SeatParameter parameter, Page? page)
    {
        if (parameter.OrderBy is not null)
        {
            try
            {
                var property = typeof(Seat).GetTypeInfo().GetProperty(parameter.OrderBy);
                var orderExpressionParam = Expression.Parameter(typeof(Seat));
                var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                var conversion = Expression.Convert(propertyAccess, typeof(object));
                var orderLambda = Expression.Lambda<Func<Seat, object?>>(conversion, orderExpressionParam);
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
