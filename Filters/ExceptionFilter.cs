using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Notify;

namespace ApiActions;

public class ExceptionFilter : IExceptionFilter
{
    private static readonly Notification DefaultError = new
    (
        "Internal Server Error",
        string.Empty,
        "Ocorreu um erro inesperado, entre em contato com o administrador"
    );

    private readonly IHostingEnvironment _env;

    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(IHostingEnvironment env, ILogger<ExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
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