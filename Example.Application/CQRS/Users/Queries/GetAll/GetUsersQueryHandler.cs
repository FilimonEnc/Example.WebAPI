using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Example.Application.Interfaces;
using Example.Application.Models.Users;
using Example.Core.Entities;

namespace Example.Application.CQRS.Users.Queries.GetAll
{
    public class GetUsersQueryHandler : BaseHandlerWithDB, IRequestHandler<GetUsersQuery, IEnumerable<UserModel>>
    {
        public GetUsersQueryHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }
        public async Task<IEnumerable<UserModel>> Handle(GetUsersQuery command, CancellationToken cancellationToken)
        {
            IQueryable<User> usersQuery = Context.Users.AsQueryable();

            IEnumerable<User> users = await usersQuery
                                            .ToArrayAsync(cancellationToken);

            return await Mapper.From(users).AdaptToTypeAsync<IEnumerable<UserModel>>();
        }
    }
}
