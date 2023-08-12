using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;

using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Queries.GetAll
{
    public class GetUsersQueryAccess : AbstractAccessValidator<GetUsersQuery>
    {
        public GetUsersQueryAccess(ICurrentUserService currentUser)
        {
            AddRule(StandartPolicies.OnlyAdminAccess(currentUser));
        }

    }
}
