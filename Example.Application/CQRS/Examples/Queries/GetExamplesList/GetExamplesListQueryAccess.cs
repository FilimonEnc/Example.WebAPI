using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;
using Example.Application.Interfaces;
using Example.Application.Models;


namespace Example.Application.CQRS.Examples.Queries.GetExamplesList
{
    public class GetExamplesListQueryAccess : AbstractAccessValidator<GetExamplesListQuery>
    {
        public GetExamplesListQueryAccess(ICurrentUserService currentUser, ICrmContext context)
        {
            AddRule(StandartPolicies.OnlyAuthenticated(currentUser));

            AddRule(() =>
            {
                switch (currentUser.UserRole)
                {
                    case UserRoles.Admin:
                        return true;
                    default:
                        Command.UserId = currentUser.UserId;
                        return true;
                }
            }, "Вы не можете просматривать чужие примеры");
        }

    }
}
