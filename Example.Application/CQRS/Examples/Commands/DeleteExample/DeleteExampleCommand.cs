using MediatR;

namespace Example.Application.CQRS.Examples.Commands.DeleteExample
{
    /// <summary>
    /// Команда удаления
    /// </summary>
    public class DeleteExampleCommand : IRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ExampleId { get; set; }
    }
}
