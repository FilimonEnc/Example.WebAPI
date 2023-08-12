using MediatR;

using System.Collections.Generic;

using Example.Application.Models.Users;

namespace Example.Application.CQRS.Users.Queries.GetAll
{
    /// <summary>
    /// Запрос на получение всех пользователей
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<UserModel>>
    {
    }
}