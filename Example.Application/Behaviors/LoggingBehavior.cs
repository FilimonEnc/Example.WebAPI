using MediatR;

using Serilog;

using System.Threading;
using System.Threading.Tasks;

using Example.Application.Interfaces;

namespace Example.Application.Behaviors
{

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUser;
        public LoggingBehavior(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            Log.Information("Запрос: {Name}. Пользователь {@UserId}.", requestName, _currentUser.UserId);

            return await next();
        }
    }
}
