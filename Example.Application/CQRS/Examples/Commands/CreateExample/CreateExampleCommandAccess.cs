using Example.Application.AccessValidation;
using Example.Application.AccessValidation.Policies;
using Example.Application.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Example.Application.CQRS.Examples.Commands.CreateExample
{
    public class CreateExampleCommandAccess : AbstractAccessValidator<CreateExampleCommand>
    {
        public CreateExampleCommandAccess(ICurrentUserService currentUser, ICrmContext context)
        {
            AddRule(StandartPolicies.OnlyAuthenticated(currentUser));

            AddRule(async () =>
            {
                if (currentUser.UserRole == Models.UserRoles.Admin) return true;
                var user = await context.Users.Where(x => x.Id == currentUser.UserId).FirstOrDefaultAsync(CancellationToken);
                if (user == null) return false;

                return false;
            },
           "Вы не можете создать Example");
        }

    }
}
