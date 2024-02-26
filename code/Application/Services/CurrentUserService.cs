using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private const string nameClaimType = "id";
    private const string idClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    private readonly IHttpContextAccessor _httpContextAccessor;

    private ClaimsPrincipal _user => _httpContextAccessor?.HttpContext?.User;

    public List<Claim> ClaimsPrincipal => _user.Claims.ToList();
    public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;
    public string UserName => _user?.FindFirst(nameClaimType)?.Value.ToUpper();
    public string tenantId => _user?.Claims.ToList().Where(x => x.Type == "tenant").FirstOrDefault().Value;
    public Guid UserId
    {
        get
        {
            var claimValue = _user?.FindFirst(idClaimType)?.Value;
            return string.IsNullOrEmpty(claimValue) ? default : new Guid(claimValue);
        }
    }

    string? ICurrentUserService.tenantId => throw new NotImplementedException();
    string? ICurrentUserService.UserId => throw new NotImplementedException();
}
