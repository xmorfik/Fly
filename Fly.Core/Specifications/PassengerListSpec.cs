using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications
{
    public class PassengerListSpec : Specification<Passenger>
    {
        public PassengerListSpec(PassengerParameter parameter, Page? page)
        {
            Query.Include(x => x.User);

            Query.Where(x => parameter.UserId == null || x.UserId == parameter.UserId);

            if (page != null)
            {
                try
                {
                    var property = typeof(Passenger).GetTypeInfo().GetProperty(parameter.OrderBy);
                    var orderExpressionParam = Expression.Parameter(typeof(Passenger));
                    var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                    var conversion = Expression.Convert(propertyAccess, typeof(object));
                    var orderLambda = Expression.Lambda<Func<Passenger, object?>>(conversion, orderExpressionParam);
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
}
