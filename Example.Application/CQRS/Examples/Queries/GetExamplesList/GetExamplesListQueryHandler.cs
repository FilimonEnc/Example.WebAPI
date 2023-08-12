using Example.Application.Interfaces;
using Example.Application.Models.Examples;
using Example.Core.Entities;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.CQRS.Examples.Queries.GetExamplesList
{
    public class GetExamplesListQueryHandler : BaseHandlerWithDBAndCurrentUser, IRequestHandler<GetExamplesListQuery, IEnumerable<ExampleModel>>
    {
        public GetExamplesListQueryHandler(ICrmContext context, ICurrentUserService currentUser, IMapper mapper) : base(context, currentUser, mapper) { }

        public async Task<IEnumerable<ExampleModel>> Handle(GetExamplesListQuery command, CancellationToken cancellationToken)
        {
            IQueryable<ExampleEntity> exaplesQuery = Context.Examples
                                                                .Include(x => x.User);

            if (command.UserId.HasValue)
                exaplesQuery = exaplesQuery.Where(x => x.UserId == command.UserId);

            else if (CurrentUser.UserRole == Models.UserRoles.User)
                exaplesQuery = exaplesQuery.Where(x => x.UserId == CurrentUser.UserId);

            IEnumerable<ExampleEntity> exampleEntities = await exaplesQuery.OrderByDescending(x => x.Id).ToArrayAsync(cancellationToken);

            return await Mapper.From(exampleEntities).AdaptToTypeAsync<IEnumerable<ExampleModel>>();
        }
    }
}
