using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        ITokenService tokenService,
        ICurrentUserService currentUser,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _tokenService = tokenService;
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user != null)
        {
            return await DeleteUserAsync(user);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }


    public async Task LogoutUserAsync()
    {
        var user = await _userManager.FindByIdAsync(_currentUser.UserId);

        if (user is not null)
        {
            await _userManager.RemoveLoginAsync(user, user.Id, user.Id);

            await _userManager.RemoveAuthenticationTokenAsync(user, user.Id, user.Id);

            return;
        }

        throw new NotFoundException("User", user.Id);
    }

    public async Task AddToRoleAsync(string userID, string role)
    {
        var user = await _userManager.FindByIdAsync(userID);

        if (user is not null)
        {
            await _userManager.AddToRoleAsync(user, role);

            return;
        }
        throw new NotFoundException("User", user.Id);
    }

    public async Task<List<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is not null)
            return (await _userManager.GetRolesAsync(user)).ToList();

        throw new NotFoundException(nameof(user), userId);
    }

    public async Task<List<string>> GetCurrentUserRolesAsync()
    {
        try
        {
            return await GetUserRolesAsync(_currentUser.UserId);
        }
        catch
        {
            throw new UnauthorizedAccessException();
        }
    }

    public async Task<string> GetCurrentUserMainRoleAsync()
    {
        var roles = await GetCurrentUserRolesAsync();

        return roles[0];
    }

    public async Task<Result> RemoveFromRoleAsync(string userID, string role)
    {
        var user = await _userManager.FindByIdAsync(userID);

        if (user is null)
            throw new NotFoundException(nameof(user), userID);

        var result = await _userManager.RemoveFromRoleAsync(user, role);

        if (result.Succeeded)
            return Result.Success();

        return Result.Failure(result.Errors.ToList()[0].Description);
    }
}
