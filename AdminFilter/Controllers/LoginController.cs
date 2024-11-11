using Microsoft.AspNetCore.Mvc;
using AdminFilter.Models;


namespace AdminFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // API controller attribute
    public class LoginController : ControllerBase
    {
        // Hardcoded users for demonstration. In a real application, use a database.
        private static List<Admin> adminList = new List<Admin>
        {
            new Admin { UN = "admin", Password = "password123", admin_status = 1 },
            new Admin { UN = "user", Password = "userpassword", admin_status = 0 }
        };

        // Handle the login POST request
        [HttpPost]
        public IActionResult Login([FromBody] Admin admin)
        {
            // Validate the user
            var loggedInUser = adminList.Find(u => u.UN == admin.UN && u.Password == admin.Password);
            
            if (loggedInUser != null)
            {
                // Store user info in session for logged-in state
                HttpContext.Session.SetString("UN", loggedInUser.UN);
                HttpContext.Session.SetInt32("Adm", loggedInUser.admin_status);

                // Return a success response with username and status
                return Ok(new { message = "Login successful", username = loggedInUser.UN });
            }
            else
            {
                // Invalid login
                return Unauthorized(new { message = "Invalid UN or password." });
            }
        }

        // Show the welcome message after successful login
        [AdminOnly]
        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            var UN = HttpContext.Session.GetString("UN");

            // If no user is logged in, return Unauthorized
            if (string.IsNullOrEmpty(UN))
            {
                return Unauthorized(new { message = "You must be logged in to access this page." });
            }

            return Ok($"Welcome {UN}");
        }

        // Handle logout
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Remove the session data
            HttpContext.Session.Remove("UN");
            HttpContext.Session.Remove("Adm");

            // Return a success response
            return Ok(new { message = "You have been logged out successfully." });
        }
    }
}