using Example.Application.Interfaces;
using Example.Application.Models;
using Example.Application.Models.Users;

using System.Security.Claims;


namespace Example.WebAPI.Utils
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _context;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor.HttpContext!;
        }

        public bool IsAuthenticated => _context.User.Identity.IsAuthenticated;
        public int UserId => !_context.User.Identity.IsAuthenticated ? 0 : int.Parse(_context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public string UserEmail => !_context.User.Identity.IsAuthenticated ? string.Empty : _context.User.FindFirst(ClaimTypes.Email).Value;
        public UserRoles UserRole => !_context.User.Identity.IsAuthenticated ? UserRoles.None : UserModel.GetRole(_context.User.FindFirst(ClaimTypes.Role).Value);
    }
}
