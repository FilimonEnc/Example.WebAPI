using Mapster;

using MapsterMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Example.Application.Interfaces;
using Example.Application.Models.Users;
using Example.Core.Entities;

namespace Example.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : BaseHandlerWithDB, IRequestHandler<DeleteUserCommand, UserModel>
    {
        public DeleteUserCommandHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<UserModel> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            User User = new() { Id = command.UserId };
            Context.Users.Attach(User);
            Context.Users.Remove(User);
            await Context.SaveChangesAsync(cancellationToken);

            return await Mapper.From(User).AdaptToTypeAsync<UserModel>();
        }


    }
}
