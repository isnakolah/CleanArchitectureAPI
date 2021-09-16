using Application.Common.Constants;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class IdentityRoles
    {
        public static readonly IdentityRole SystemAdmin = new(Roles.SystemAdminRole);
    }
}
