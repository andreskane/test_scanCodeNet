using System.Security.Claims;

namespace Application.Interfaces.Services;

public interface ICurrentUserService
{

    string? UserId { get; }

    string UserName { get; }
    string? tenantId { get; }
    List<Claim> ClaimsPrincipal { get; }
}
