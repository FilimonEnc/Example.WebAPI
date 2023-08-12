using Example.Application.Models.Examples;

using MediatR;

namespace Example.Application.CQRS.Examples.Queries.GetExample
{
    /// <summary>
    /// Запрос на получение данных 
    /// </summary>
    public class GetExampleQuery : IRequest<ExampleModel>
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public int ExampleId { get; set; }
    }
}
