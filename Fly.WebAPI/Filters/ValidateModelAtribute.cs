using Microsoft.AspNetCore.Mvc.Filters;

namespace Fly.WebAPI.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {

        }
        base.OnActionExecuting(context);
    }

}
