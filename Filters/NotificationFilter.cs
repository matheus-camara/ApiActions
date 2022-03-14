using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Notify;

namespace ApiActions;

public class CustomNotificationFilter : ActionFilterAttribute
{
    private readonly INotificationContext _notificationContext;

    public CustomNotificationFilter(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (_notificationContext.HasNotifications)
        {
            var result = new ObjectResult(_notificationContext.Notifications.Select(x => x.Message).ToList());
            result.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.Result = result;
        }
        else
        {
            base.OnActionExecuted(context);
        }
    }
}