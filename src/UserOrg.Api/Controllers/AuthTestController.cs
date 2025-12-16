using Microsoft.AspNetCore.Mvc;

namespace UserOrg.Api.Controllers;

[ApiController]
[Route("api")]
public class AuthTestController : ControllerBase
{
    [HttpGet("secure-test")]
    public IActionResult SecureTest()
    {
        var uid = HttpContext.Items["FirebaseUid"] as string;
        if (string.IsNullOrEmpty(uid))
            return Unauthorized("Missing/invalid Firebase token");

        var email = HttpContext.Items["FirebaseEmail"] as string;
        return Ok(new { message = "Authorized", uid, email });
    }
}
