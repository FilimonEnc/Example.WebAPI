using Example.Application.Models;

namespace Example.Application.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string UserEmail { get; }
        UserRoles UserRole { get; }
        bool IsAuthenticated { get; }
    }
}
