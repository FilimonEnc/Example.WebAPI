using Example.Application.Extensions;
using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : BaseValidationWithDB<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.UserId).EntityExist(context.Users);
        }
    }
}
