using Example.Application.Interfaces;

using MapsterMapper;

using System;

namespace Example.Application.CQRS
{
    public abstract class BaseHandlerWithDBAndCurrentUser
    {
        protected readonly ICrmContext Context;
        protected readonly IMapper Mapper;
        protected readonly ICurrentUserService CurrentUser;

        public BaseHandlerWithDBAndCurrentUser(ICrmContext context, ICurrentUserService currentUser, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

    }
}
