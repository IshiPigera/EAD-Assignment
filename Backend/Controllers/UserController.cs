using Microsoft.AspNetCore.Mvc;
using TravelerAppService.Models;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppWebService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUsers( User user)
        {
            try
            { // You can replace this with your user creation logic

                // Call the UserService to create a new user asynchronously
                await _userService.CreateAsync(user);

                Console.WriteLine($"Received user data: Id = {user.Id}, Name = {user.FirstName}, Email = {user.Email}, etc.");

                // Return HTTP 201 Created status along with the newly created user
                return Ok(new { Message = "User created Successfully", user });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateAsync(id, user);
            return Ok(new { Message = "User Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteAsync(id);
            return Ok(new { Message = "User Deleted Successfully" });

        }
    }

}
