using Example.Application.Interfaces;

using FluentValidation;

namespace Example.Application.CQRS.Users.Queries.Authenticate
{
    public class AuthenticateQueryValidator : BaseValidationWithDB<AuthenticateQuery>
    {
        public AuthenticateQueryValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
