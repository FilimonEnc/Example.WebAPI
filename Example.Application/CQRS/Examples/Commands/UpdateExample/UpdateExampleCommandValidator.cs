using Example.Application.Extensions;
using Example.Application.Interfaces;

namespace Example.Application.CQRS.Examples.Commands.UpdateExample
{
    public class UpdateExampleCommandValidator : BaseValidationWithDB<UpdateExampleCommand>
    {
        public UpdateExampleCommandValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.ExampleId).EntityExist(context.Examples);
        }
    }
}
