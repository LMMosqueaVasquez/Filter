using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController] 
public class TestAdminController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> SeeStatus()
    {
        var UserN = HttpContext.Session.GetString("UN");
        // Get the admin status from the session
        var adminStatus = HttpContext.Session.GetInt32("Adm");

        // Check if the user is an admin
        if (adminStatus == 1)
        {
            // Return success response with the admin details
            return Ok(new { message = $"Welcome {UserN}"});
        }
        else
        {
            // Return BadRequest if the user is not an admin
            return BadRequest("Not an admin");
        }
    }
}