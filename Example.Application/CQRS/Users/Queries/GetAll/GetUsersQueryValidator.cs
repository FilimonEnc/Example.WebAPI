using Example.Application.Interfaces;

namespace Example.Application.CQRS.Users.Queries.GetAll
{
    public class GetUsersQueryValidator : BaseValidationWithDB<GetUsersQuery>
    {
        public GetUsersQueryValidator(ICrmContext context) : base(context)
        {

        }
    }
}
