using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace Example.WebAPI.Utils
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            if (identity is ClaimsIdentity ci)
            {
                var id = ci.FindFirst(ClaimTypes.NameIdentifier);
                if (id != null)
                {
                    return Convert.ToInt32(id.Value, CultureInfo.InvariantCulture);
                }
            }
            return 0;
        }
        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            string role = "";
            if (identity is ClaimsIdentity ci)
            {
                var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
                if (id != null)
                    role = id.Value;
            }
            return role;
        }
    }
}
