using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Serilog;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validators = _validators
                .Select(async v => await v.ValidateAsync(context));

            List<ValidationFailure> failures = new List<ValidationFailure>();
            foreach (var item in validators)
            {
                var result = await item;
                var fails = result.Errors.Where(failure => failure != null).ToList();
                failures.AddRange(fails);
            }

            if (failures.Count != 0)
            {
                var requestName = typeof(TRequest).Name;
                Log.Information("Запрос: {Name}. Пользователь {@Request}.", requestName, request);
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}