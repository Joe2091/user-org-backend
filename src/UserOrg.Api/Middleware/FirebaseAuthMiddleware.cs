using FirebaseAdmin.Auth;

namespace UserOrg.Api.Middleware;

public class FirebaseAuthMiddleware
{
    private readonly RequestDelegate _next;

    public FirebaseAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();

        if (!string.IsNullOrWhiteSpace(authHeader) &&
            authHeader.StartsWith("Bearer "))
        {
            var token = authHeader["Bearer ".Length..].Trim();

            try
            {
                var decodedToken =
                    await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                //exposing Firebase info to controllers
                context.Items["FirebaseUid"] = decodedToken.Uid;
                context.Items["FirebaseEmail"] =
                    decodedToken.Claims.TryGetValue("email", out var email)
                        ? email?.ToString()
                        : null;
            }
            catch
            {
                // Invalid token = do nothing (request will be unauthorized later)
            }
        }

        await _next(context);
    }
}
