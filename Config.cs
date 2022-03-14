using ApiActions.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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

    public static IServiceCollection AddApiActions(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}