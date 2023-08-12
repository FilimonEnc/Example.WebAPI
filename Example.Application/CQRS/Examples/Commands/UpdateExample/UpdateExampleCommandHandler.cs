using Example.Application.Exceptions;
using Example.Application.Interfaces;
using Example.Application.Models.Examples;
using Example.Core.Entities;

using Mapster;

using MapsterMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.CQRS.Examples.Commands.UpdateExample
{
    public class UpdateExampleCommandHandler : BaseHandlerWithDBAndCurrentUser, IRequestHandler<UpdateExampleCommand, ExampleModel>
    {

        public UpdateExampleCommandHandler(ICrmContext context, ICurrentUserService currentUser, IMapper mapper) : base(context, currentUser, mapper) { }

        public async Task<ExampleModel> Handle(UpdateExampleCommand command, CancellationToken cancellationToken)
        {
            ExampleEntity? exampleEntity = await Context.Examples.FindAsync(new object[] { command.ExampleId }, cancellationToken);

            if (exampleEntity == null)
                throw new NotFoundException($"Заказ с ID {command.ExampleId} не найден");

            Mapper.Map(command, exampleEntity);

            await Context.SaveChangesAsync(cancellationToken);

            await Context.Entry(exampleEntity).Reference(x => x.User).LoadAsync(cancellationToken);

            return await Mapper.From(exampleEntity).AdaptToTypeAsync<ExampleModel>();
        }
    }
}
