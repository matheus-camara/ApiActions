using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiActions
{
    public abstract class MediatorController : ControllerBase
    {
        protected IMediator Mediator { get; init; }
        protected MediatorController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}