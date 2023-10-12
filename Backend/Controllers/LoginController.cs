using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using TravelerAppWebService.Models;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppWebService.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Login>> Login(Login loginUser)
        {
            if (loginUser != null && !string.IsNullOrEmpty(loginUser.NationalIdentificationCard) && _userService != null)
            {
                var user = await _userService.GetByIdAsync(loginUser.NationalIdentificationCard);

                if (user != null) {
                    if (VerifyPassword(loginUser.Password, user.Password))
                    {
                        return Ok( new { Message = "Login Successfully", user });

                    }
                    else { 
                    return Unauthorized(new { Message = "Invalid Password" });

                    }

                }
                else {
                    return Unauthorized(new { Message = "Invalid NIC" });
                }
                
            }
            // Replace this with your authentication logic
            else {
                return Unauthorized();
            }

            
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Replace this with your actual password verification logic.
            // You should compare the entered password with the stored password hash securely.
            // This is a simplified example.
            return enteredPassword == storedPassword;
        }



    }
}
