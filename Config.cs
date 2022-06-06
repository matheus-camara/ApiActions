using Microsoft.AspNetCore.Mvc;

namespace ApiActions;

public static class Config
{
    public static MvcOptions AddApiActions(this MvcOptions options)
    {
        options.Filters.Add(typeof(CustomNotificationFilter));
        options.Filters.Add(typeof(NotFoundFilter));
        options.Filters.Add(typeof(NoContentFilter));
        options.Filters.Add(typeof(ExceptionFilter));
        return options;
    }
}