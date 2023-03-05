using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using StackExchange.Redis;
using StackExchange.Redis.Profiling;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class AirlineListSpec : Specification<Airline>
{
    public AirlineListSpec(AirlineParameter parameter, Page? page)
    {        
        var paramEx = Expression.Parameter(typeof(PropertyInfo));
        var memberEx = Expression.PropertyOrField(paramEx, "PropertyType");
        var constantEx = Expression.Constant(Type.GetType("System.String"));
        var binaryEx = Expression.Equal(memberEx, constantEx);
        var stringProp = Expression.Lambda<Func<PropertyInfo, bool>>(binaryEx, paramEx);

        var props = typeof(AirlineParameter).GetProperties();
        var strProps = props.AsQueryable().Where(stringProp).ToList();

        foreach (var prop in strProps)
        {
            if(prop.GetValue(parameter) == null)
            {
                continue;
            }

            var airlineExpression = Expression.Parameter(typeof(Airline));

            try
            {
                var memberExpression = Expression.PropertyOrField(airlineExpression, prop.Name);
                var constantExpression = Expression.Constant(prop.GetValue(parameter));
                var binaryExpression = Expression.Equal(memberExpression, constantExpression);
                var lambda = Expression.Lambda<Func<Airline, bool>>(binaryExpression, airlineExpression);
                Query.Where(lambda);
            }
            catch
            {
                continue;
            }
        }

        if (page != null)
        {
            if(parameter.OrderBy != null)
            {
                try
                {
                    var property = typeof(Airline).GetTypeInfo().GetProperty(parameter.OrderBy);
                    var orderExpressionParam = Expression.Parameter(typeof(Airline));
                    var propertyAccess = Expression.MakeMemberAccess(orderExpressionParam, property);
                    var conversion = Expression.Convert(propertyAccess, typeof(object));
                    var orderLambda = Expression.Lambda<Func<Airline, object?>>(conversion, orderExpressionParam);
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
                    Query.OrderByDescending(x=>x.Id);
                }
            }
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
        }
    }
}
