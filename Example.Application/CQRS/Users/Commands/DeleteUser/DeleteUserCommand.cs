using MediatR;

using Example.Application.Models.Users;

namespace Example.Application.CQRS.Users.Commands.DeleteUser
{
    /// <summary>
    /// Команда удаления пользователя
    /// </summary>
    public class DeleteUserCommand : IRequest<UserModel>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
