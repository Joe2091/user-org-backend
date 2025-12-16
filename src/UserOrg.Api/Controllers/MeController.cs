using Microsoft.AspNetCore.Mvc;
using UserOrg.Api.Auth;

namespace UserOrg.Api.Controllers;

[ApiController]
[Route("api")]
public class MeController : ControllerBase
{
    [HttpGet("me")]
    [RequireFirebaseAuth]
    public IActionResult Me()
    {
        var user = HttpContext.GetFirebaseUser()!;
        return Ok(new { uid = user.Uid, email = user.Email });
    }
}
