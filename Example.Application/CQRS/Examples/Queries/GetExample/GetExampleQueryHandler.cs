using Example.Application.Interfaces;
using Example.Application.Models.Examples;
using Example.Core.Entities;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.CQRS.Examples.Queries.GetExample
{
    public class GetExampleQueryHandler : BaseHandlerWithDB, IRequestHandler<GetExampleQuery, ExampleModel>
    {
        public GetExampleQueryHandler(ICrmContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ExampleModel> Handle(GetExampleQuery command, CancellationToken cancellationToken)
        {
            ExampleEntity? exampleEntity = await Context.Examples.Where(x => x.Id == command.ExampleId)
                                                .Include(x => x.User)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (exampleEntity == null) throw new Exceptions.NotFoundException($"Пример с номером {command.ExampleId} не найден");
            return await Mapper.From(exampleEntity).AdaptToTypeAsync<ExampleModel>();
        }
    }
}
