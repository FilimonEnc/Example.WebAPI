using Example.Application.Models.Base;

namespace Example.Application.Models.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserModel : BaseModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Роль
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public UserRoles UserRole => GetRole(Role);

        public static UserRoles GetRole(string role)
        {
            return role switch
            {
                "User" => UserRoles.User,
                "Admin" => UserRoles.Admin,
                _ => UserRoles.None,
            };
        }
    }
}
