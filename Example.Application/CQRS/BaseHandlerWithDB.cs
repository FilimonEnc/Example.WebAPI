using MapsterMapper;

using System;

using Example.Application.Interfaces;

namespace Example.Application.CQRS
{
    public abstract class BaseHandlerWithDB
    {
        protected readonly ICrmContext Context;
        protected readonly IMapper Mapper;

        public BaseHandlerWithDB(ICrmContext context, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

    }
}
