using FluentValidation;
using MediatR;
using Notify;

namespace ApiActions.Pipelines;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly INotificationContext _notificationContext;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
        INotificationContext notificationContext)
    {
        _validators = validators;
        _notificationContext = notificationContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);
        var task_validations = _validators
            .Select(v => v.ValidateAsync(context, cancellationToken));

        var result = await Task.WhenAll(task_validations);

        var failures = result.SelectMany(result => result.Errors)
                        .Where(f => f != null)
                        .ToList();

        if (failures.Any())
        {
            foreach (var failure in failures)
                _notificationContext.AddNotification(failure.PropertyName, failure.ErrorMessage);

            return default(TResponse)!;
        }

        return await next.Invoke();
    }
}