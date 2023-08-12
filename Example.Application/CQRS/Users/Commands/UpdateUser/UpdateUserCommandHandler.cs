using Mapster;

using MapsterMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Example.Application.Exceptions;
using Example.Application.Interfaces;
using Example.Application.Models.Users;
using Example.Core.Entities;

namespace Example.Application.CQRS.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : BaseHandlerWithDB, IRequestHandler<UpdateUserCommand, UserModel>
{
    public UpdateUserCommandHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<UserModel> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await Context.Users.FindAsync(new object[] { command.Id }, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException($"Работника с Id {command.Id} не найден");
        }

        Mapper.Map(command, user);
        await Context.SaveChangesAsync(cancellationToken);

        return await Mapper.From(user).AdaptToTypeAsync<UserModel>();
    }


}