using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;
using Example.Application.Interfaces;


namespace Example.Application.CQRS.Examples.Commands.DeleteExample
{
    public class DeleteExampleCommandAccess : AbstractAccessValidator<DeleteExampleCommand>
    {
        public DeleteExampleCommandAccess(ICurrentUserService currentUser)
        {
            AddRule(StandartPolicies.OnlyAuthenticated(currentUser));
        }

    }
}
