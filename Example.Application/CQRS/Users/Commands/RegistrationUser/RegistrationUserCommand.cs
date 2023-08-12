using MediatR;

using Example.Application.Models;
using Example.Application.Models.Users;

namespace Example.Application.CQRS.Users.Commands.RegistrationUser
{
    /// <summary>
    /// Команда регистрации нового пользователя
    /// </summary>
    public class RegistrationUserCommand : IRequest<UserModel>
    {
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
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; } = string.Empty;

        public int FilialId { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}

