using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;

using Example.Application.Interfaces;


namespace Example.Application.CQRS.Examples.Queries.GetExample
{
    public class GetExampleQueryAccess : AbstractAccessValidator<GetExampleQuery>
    {
        public GetExampleQueryAccess(ICurrentUserService currentUser)
        {
            AddRule(StandartPolicies.OnlyAuthenticated(currentUser));
        }

    }
}
