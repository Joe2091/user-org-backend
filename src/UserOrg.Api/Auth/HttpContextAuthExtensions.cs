using Microsoft.AspNetCore.Http;

namespace UserOrg.Api.Auth;

public static class HttpContextAuthExtensions
{
    public static FirebaseUser? GetFirebaseUser(this HttpContext context)
    {
        var uid = context.Items["FirebaseUid"] as string;
        if (string.IsNullOrWhiteSpace(uid)) return null;

        var email = context.Items["FirebaseEmail"] as string;
        return new FirebaseUser(uid, email);
    }
}
