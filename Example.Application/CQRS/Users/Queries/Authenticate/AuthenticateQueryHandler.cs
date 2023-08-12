using Example.Application.Exceptions;
using Example.Application.Interfaces;
using Example.Application.Models.Users;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.CQRS.Users.Queries.Authenticate
{
    public class AuthenticateQueryHandler : BaseHandlerWithDB, IRequestHandler<AuthenticateQuery, CurrentUser>
    {
        public AuthenticateQueryHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }
        
        public async Task<CurrentUser> Handle(AuthenticateQuery command, CancellationToken cancellationToken)
        {
            var userEntity = await Context.Users.FirstOrDefaultAsync(x => x.Login == command.Login &&
                                                                         x.Password == command.Password, cancellationToken);

            if (userEntity == null)
            {
                throw new NotFoundException("Пользователь с таким логином и паролем не существует");
            }
            return await Mapper.From(userEntity).AdaptToTypeAsync<CurrentUser>();
        }
    }
}
