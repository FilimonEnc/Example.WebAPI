using Example.Application.Models.Examples;

using MediatR;


namespace Example.Application.CQRS.Examples.Commands.CreateExample
{
    /// <summary>
    /// Команда создания примера
    /// </summary>
    public class CreateExampleCommand : IRequest<ExampleModel>
    {

        /// <summary>
        /// Пример имени
        /// </summary>
        public string? ExampleName { get; set; } = string.Empty;

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }



    }
}
