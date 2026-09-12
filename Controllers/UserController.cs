using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterviewManagementPortal.Data;
using InterviewManagementPortal.Models;
using InterviewManagementPortal.Models.DTOs;

namespace InterviewManagementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _context.Users
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    Title = user.Title,
                    Department = user.Department,
                    CreatedAt = user.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(user => user.Id == id)
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    Title = user.Title,
                    Department = user.Department,
                    CreatedAt = user.CreatedAt
                })
                .FirstOrDefaultAsync();

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
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role,
                Title = userDto.Title,
                Department = userDto.Department
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Title = user.Title,
                Department = user.Department,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                response
            );
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(
            int id,
            UpdateUserDto userDto)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.Password = userDto.Password;
            existingUser.Role = userDto.Role;
            existingUser.Title = userDto.Title;
            existingUser.Department = userDto.Department;

            await _context.SaveChangesAsync();

            var response = new UserResponseDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                Role = existingUser.Role,
                Title = existingUser.Title,
                Department = existingUser.Department,
                CreatedAt = existingUser.CreatedAt
            };

            return Ok(response);
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User deleted successfully"
            });
        }
    }
}
