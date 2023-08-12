using Example.Application.Models.Examples;

using MediatR;

namespace Example.Application.CQRS.Examples.Commands.UpdateExample
{
    /// <summary>
    /// Команда сохранения
    /// </summary>
    public class UpdateExampleCommand : IRequest<ExampleModel>
    {
        /// <summary>
        /// Индифекато
        /// </summary>
        public int ExampleId { get; set; }

        /// <summary>
        /// пример
        /// </summary>
        public string? SimpleName { get; set; } = string.Empty;


    }
}
