using System.Collections.Generic;

namespace Example.Core.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity
    {
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; } = string.Empty;

        public List<ExampleEntity> Examples { get; set; } = null!;
    }
}
