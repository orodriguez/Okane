using System.Security.Claims;
using ISession = Okane.Core.Security.ISession;

namespace Okane.WebApi;

public class HttpContextSession : ISession
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextSession(IHttpContextAccessor contextAccessor) => 
        _contextAccessor = contextAccessor;

    public int GetCurrentUserId()
    {
        var nameIdentifierClaim = _contextAccessor
            .HttpContext!
            .User
            .FindFirst(ClaimTypes.NameIdentifier);
        
        return int.Parse(nameIdentifierClaim!.Value);
    }
}