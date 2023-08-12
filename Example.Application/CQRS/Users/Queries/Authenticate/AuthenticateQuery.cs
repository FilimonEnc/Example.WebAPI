using MediatR;

using Example.Application.Models.Users;

namespace Example.Application.CQRS.Users.Queries.Authenticate
{
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    public class AuthenticateQuery : IRequest<CurrentUser>
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
