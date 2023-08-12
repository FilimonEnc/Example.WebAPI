using Example.Application.Interfaces;
using Example.Application.Models.Examples;
using Example.Core.Entities;

using Mapster;

using MapsterMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.CQRS.Examples.Commands.CreateExample
{

    public class CreateExampleCommandHandler : BaseHandlerWithDBAndCurrentUser, IRequestHandler<CreateExampleCommand, ExampleModel>
    {
        public CreateExampleCommandHandler(ICrmContext context, IMapper mapper, ICurrentUserService currentUser) : base(context, currentUser, mapper) { }

        public async Task<ExampleModel> Handle(CreateExampleCommand command, CancellationToken cancellationToken)
        {
            ExampleEntity example = new()
            {
                UserId = command.UserId,
                ExampleName = command.ExampleName!
            };

            await Context.Examples.AddAsync(example, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            await Context.Entry(example).Reference(x => x.User).LoadAsync(cancellationToken);

            return await Mapper.From(example).AdaptToTypeAsync<ExampleModel>();
        }

    }
}
