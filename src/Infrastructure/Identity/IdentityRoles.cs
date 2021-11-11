using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityRoles
{
    public static readonly IdentityRole SystemAdmin = new(Roles.SystemAdminRole);
}
