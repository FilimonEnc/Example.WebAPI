using Example.Application.AccessValidation;
using Example.Application.Exceptions;
using Example.Application.Interfaces;

using MediatR;

using Serilog;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.Behaviors
{
    public class AccessBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IAccessValidator<TRequest>> _validators;
        private readonly ICurrentUserService _currentUser;

        public AccessBehavior(IEnumerable<IAccessValidator<TRequest>> validators, ICurrentUserService currentUser)
        {
            _validators = validators;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            foreach (var item in _validators)
            {
                if (!await item.Valiadate(request, cancellationToken))
                {
                    var requestName = typeof(TRequest).Name;
                    Log.Warning("Отказ в доступе: {Name}. Пользователь {@UserId}. {@Request}", requestName, _currentUser.UserId, request);
                    throw new ForbiddenException(item.ErrorMessage);
                }
            }

            return await next();
        }
    }
}