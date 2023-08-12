using Example.Application.Models.Examples;

using MediatR;

using System.Collections.Generic;

namespace Example.Application.CQRS.Examples.Queries.GetExamplesList
{
    /// <summary>
    /// Запрос на получение списка примеров зарегистрированного пользователя
    /// </summary>
    public class GetExamplesListQuery : IRequest<IEnumerable<ExampleModel>>
    {
        public int? UserId { get; set; }
    }
}
