using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserOrg.Api.Auth;
using UserOrg.Domain.Entities;
using UserOrg.Infrastructure.Persistence;

namespace UserOrg.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("sync")]
    [RequireFirebaseAuth]
    public async Task<IActionResult> Sync()
    {
        var fb = HttpContext.GetFirebaseUser()!;

        var existing = await _db.Set<User>()
            .FirstOrDefaultAsync(u => u.FirebaseUid == fb.Uid);

        if (existing == null)
        {
            // Create a new DB user linked to Firebase
            var user = new User
            {
                FirebaseUid = fb.Uid,
                Email = fb.Email ?? "",
                FirstName = "New",
                LastName = "User",
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Add(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }

        // Update email if it changed
        if (!string.IsNullOrWhiteSpace(fb.Email) && existing.Email != fb.Email)
        {
            existing.Email = fb.Email!;
            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        return Ok(existing);
    }
}
