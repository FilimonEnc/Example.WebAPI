using Example.Application.CQRS.Examples.Commands.CreateExample;
using Example.Application.Extensions;
using Example.Application.Interfaces;

namespace Example.Application.CQRS.Orders.Commands.CreateOrder
{
    public class CreateExampleCommandValidator : BaseValidationWithDB<CreateExampleCommand>
    {
        public CreateExampleCommandValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.UserId).EntityExist(context.Users);
        }
    }
}
