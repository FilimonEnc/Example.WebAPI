using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;

using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandAccess : AbstractAccessValidator<UpdateUserCommand>
    {
        public UpdateUserCommandAccess(ICurrentUserService currentUser)
        {
            AddRule(StandartPolicies.OnlyAdminAccess(currentUser));
        }

    }
}