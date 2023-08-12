using FluentValidation;

using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Commands.RegistrationUser
{
    public class RegistrationUserCommandValidator : BaseValidationWithDB<RegistrationUserCommand>
    {
        public RegistrationUserCommandValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
