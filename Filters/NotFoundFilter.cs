using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiActions;

public class NotFoundFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context is { Result: ObjectResult result } && result is { Value: null })
            context.Result = new NotFoundResult();
        else
            base.OnActionExecuted(context);
    }
}