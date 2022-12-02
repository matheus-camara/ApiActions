using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Notify;

namespace ApiActions;

public class ExceptionFilter : IExceptionFilter
{
    private Notification DefaultError => new
    (
        "Internal Server Error",
        string.Empty,
        _localizer.GetString("InternalServerError")
    );

    private readonly IHostingEnvironment _env;

    private readonly ILogger<ExceptionFilter> _logger;

    private readonly IStringLocalizer<ExceptionFilter> _localizer;

    public ExceptionFilter(IHostingEnvironment env, ILogger<ExceptionFilter> logger, IStringLocalizer<ExceptionFilter> localizer)
    {
        _env = env;
        _logger = logger;
        _localizer = localizer;
    }

    public void OnException(ExceptionContext context)
    {
        if (!_env.IsProduction()) return;

        _logger.LogError(context.Exception.Message);

        var result = new ObjectResult(DefaultError);
        result.StatusCode = 500;

        context.Result = result;
    }
}