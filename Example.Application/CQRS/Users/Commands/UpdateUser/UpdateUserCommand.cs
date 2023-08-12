using MediatR;

using Example.Application.Models;
using Example.Application.Models.Users;

namespace Example.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserModel>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Роль
        /// </summary>
        public UserRoles Role { get; set; }

    }
}
