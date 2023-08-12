using Example.Application.Extensions;
using Example.Application.Interfaces;

namespace Example.Application.CQRS.Examples.Queries.GetExample
{
    public class GetExampleQueryValidator : BaseValidationWithDB<GetExampleQuery>
    {
        public GetExampleQueryValidator(ICrmContext context) : base(context)
        {
            RuleFor(x => x.ExampleId).EntityExist(context.Examples);
        }
    }
}
