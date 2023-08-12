using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;
using Example.Application.Interfaces;


namespace Example.Application.CQRS.Examples.Commands.UpdateExample
{
    public class UpdateExampleCommandAccess : AbstractAccessValidator<UpdateExampleCommand>
    {
        public UpdateExampleCommandAccess(ICurrentUserService currentUser)
        {
            AddRule(StandartPolicies.OnlyAuthenticated(currentUser));
        }

    }
}
