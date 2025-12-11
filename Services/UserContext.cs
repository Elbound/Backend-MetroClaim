using System.Security.Claims;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid CurrentUserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return Guid.Empty;

            var idString = user.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return Guid.TryParse(idString, out var guid) ? guid : Guid.Empty;
        }
    }

    public string CurrentEmail
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        }
    }

    public string CurrentName
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? "Unknown";
        }
    }

    public IEnumerable<string> CurrentRoles
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return Enumerable.Empty<string>();

            return user.FindAll(ClaimTypes.Role).Select(c => c.Value);
        }
    }

    public bool IsInRole(string role)
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
    }
}