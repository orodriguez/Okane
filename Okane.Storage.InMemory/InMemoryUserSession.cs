using Okane.Core.Security;

namespace Okane.Storage.InMemory;

public class InMemoryUserSession : IUserSession
{
    private int _userId;

    public void SetCurrentUserId(int userId) => _userId = userId;
    public int GetCurrentUserId() => _userId;
}