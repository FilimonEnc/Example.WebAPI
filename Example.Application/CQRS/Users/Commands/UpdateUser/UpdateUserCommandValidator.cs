using Example.Application.Extensions;
using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : BaseValidationWithDB<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.Id).EntityExist(context.Users);
        }
    }
}
