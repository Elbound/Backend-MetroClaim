namespace MetroClaim.Api.Services.Interfaces;

public interface IUserContext
{
    Guid CurrentUserId { get; }
    string CurrentEmail { get; }
    string CurrentName { get; }
    IEnumerable<string> CurrentRoles { get; }
    bool IsInRole(string role);
}