using Example.Application.Interfaces;
using Example.Core.Entities;

using MapsterMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.CQRS.Examples.Commands.DeleteExample
{
    public class DeleteExampleCommandHandler : BaseHandlerWithDB, IRequestHandler<DeleteExampleCommand>
    {
        public DeleteExampleCommandHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<Unit> Handle(DeleteExampleCommand command, CancellationToken cancellationToken)
        {
            ExampleEntity entity = new() { Id = command.ExampleId };
            Context.Examples.Attach(entity);
            Context.Examples.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
