using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class AirlineListSpec : Specification<Airline>
{
    public AirlineListSpec(AirlineParameter parameter, Page? page)
    {
        var param = Expression.Parameter(typeof(PropertyInfo));
        var member = Expression.PropertyOrField(param, "PropertyType");
        var constant = Expression.Constant(Type.GetType("System.String"));
        var equal = Expression.Equal(member, constant);
        var stringProp = Expression.Lambda<Func<PropertyInfo, bool>>(equal, param);

        var properties = typeof(AirlineParameter).GetProperties();
        var stringProperties = properties.AsQueryable().Where(stringProp).ToList();

        foreach (var prop in stringProperties)
        {
            if (prop.GetValue(parameter) is null)
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
            }
        }

        if (parameter.OrderBy is not null)
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
