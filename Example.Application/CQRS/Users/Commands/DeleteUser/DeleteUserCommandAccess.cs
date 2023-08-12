using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;

using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommandAccess : AbstractAccessValidator<DeleteUserCommand>
    {
        public DeleteUserCommandAccess(ICurrentUserService currentUser)
        {
            AddRule(StandartPolicies.OnlyAdminAccess(currentUser));
        }

    }
}