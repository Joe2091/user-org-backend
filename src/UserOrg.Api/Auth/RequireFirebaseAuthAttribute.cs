using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserOrg.Api.Auth;

public class RequireFirebaseAuthAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.GetFirebaseUser();
        if (user is null)
        {
            context.Result = new UnauthorizedObjectResult("Missing/invalid Firebase token");
        }
    }
}
