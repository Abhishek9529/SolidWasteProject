using IMP.Application.Dto.Users;
using IMP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser(
            CreateUserDto userDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(userDto);

                return CreatedAtAction(
                    nameof(GetUser),
                    new { id = user.Id },
                    user
                );
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(
            int id,
            UpdateUserDto userDto)
        {
            try
            {
                var updated = await _userService.UpdateUserAsync(id, userDto);

                if (!updated)
                {
                    return NotFound(new
                    {
                        message = "User not found"
                    });
                }

                var user = await _userService.GetUserByIdAsync(id);

                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            return Ok(new
            {
                message = "User deleted successfully"
            });
        }
    }
}