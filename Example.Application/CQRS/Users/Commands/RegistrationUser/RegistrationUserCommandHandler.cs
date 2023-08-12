using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

using Example.Application.Exceptions;
using Example.Application.Interfaces;
using Example.Application.Models.Users;
using Example.Core.Entities;

namespace Example.Application.CQRS.Users.Commands.RegistrationUser
{
    public class RegistrationUserCommandHandler : BaseHandlerWithDB, IRequestHandler<RegistrationUserCommand, UserModel>
    {

        public RegistrationUserCommandHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<UserModel> Handle(RegistrationUserCommand command, CancellationToken cancellationToken)
        {
            User user = await Mapper.From(command).AdaptToTypeAsync<User>();
            user.Role = command.Role;

            var chek = await Context.Users.FirstOrDefaultAsync(x => x.Login == command.Login, cancellationToken);
            if (chek != null)
                throw new ForbiddenException("Такой пользователь уже есть");

            await Context.Users.AddAsync(user, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return await Mapper.From(user).AdaptToTypeAsync<UserModel>();
        }
    }
}
