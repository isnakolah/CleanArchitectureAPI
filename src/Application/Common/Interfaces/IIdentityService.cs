using Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<Result> RemoveFromRoleAsync(string userID, string role);

        Task<Result> DeleteUserAsync(string userId);

        Task LogoutUserAsync();

        Task AddToRoleAsync(string userID, string role);

        Task<List<string>> GetCurrentUserRolesAsync();

        Task<string> GetCurrentUserMainRoleAsync();
    }
}
