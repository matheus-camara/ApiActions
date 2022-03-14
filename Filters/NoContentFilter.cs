using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiActions;

public class NoContentFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (IsEmptyResult(context))
            context.Result = new NoContentResult();
        else
            base.OnActionExecuted(context);
    }

    private bool IsEmptyResult(ActionExecutedContext context)
    {
        return context is { Result: ObjectResult result }
               && (result is { Value: Array array } && array is { Length: 0 }
                   || result is { Value: IEnumerable enumerable } && Empty(enumerable));
    }

    private bool Empty(IEnumerable enumerable)
    {
        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator is IDisposable disposable)
                disposable.Dispose();

            return false;
        }

        return true;
    }
}