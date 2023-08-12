namespace Example.Application.Models.Users
{
    /// <summary>
    /// Текущий пользователь
    /// </summary>
    public class CurrentUser : UserModel
    {
        /// <summary>
        /// Токен пользователя
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
