using Example.Application.Extensions;
using Example.Application.Interfaces;

namespace Example.Application.CQRS.Examples.Commands.DeleteExample
{
    public class DeleteExampleCommandValidator : BaseValidationWithDB<DeleteExampleCommand>
    {
        public DeleteExampleCommandValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.ExampleId).EntityExist(context.Examples);
        }
    }
}
