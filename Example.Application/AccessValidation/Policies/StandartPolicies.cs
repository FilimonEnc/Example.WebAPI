using Example.Application.Interfaces;
using Example.Application.Models;

namespace Example.Application.AccessValidation.Policies
{
    public static class StandartPolicies
    {
        /// <summary>
        /// Проверка, является ли пользователь администратором
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public static AccessRule OnlyAdminAccess(ICurrentUserService currentUser)
        {
            return new()
            {
                ErrorMessage = "Для выполнения этой операции требуется уровень администратора",
                RuleFunction = () => currentUser.UserRole == UserRoles.Admin
            };
        }

        /// <summary>
        /// Проверка, авторизован ли пользователь
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public static AccessRule OnlyAuthenticated(ICurrentUserService currentUser)
        {
            return new()
            {
                ErrorMessage = "Для выполнения этой операции требуется вход в систему",
                RuleFunction = () => currentUser.IsAuthenticated
            };
        }

    }
}
