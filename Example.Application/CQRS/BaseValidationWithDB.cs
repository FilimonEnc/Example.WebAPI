using Example.Application.Interfaces;

using FluentValidation;

using System;

namespace Example.Application.CQRS
{
    public abstract class BaseValidationWithDB<T> : AbstractValidator<T> where T : class
    {
        public readonly ICrmContext _dbContext;
        public BaseValidationWithDB(ICrmContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
