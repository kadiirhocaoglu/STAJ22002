
using AutoMapper;
using HST.Business.Services.Abstract;
using HST.DTO.DTOs.UserDtos;
using HST.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var results = await userService.GetAllUsersWithRoleAsync();
            return Ok(results);
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
                userRegisterDto.Roles = await userService.GetAllRolesAsync();
                var result = await userService.CreateUserAsync(userRegisterDto);
                if (result.Succeeded)
                {
                    return Ok("User registered successfully");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await userService.GetAppUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (userId != userUpdateDto.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var result = await userService.UpdateUserAsync(userUpdateDto);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await userService.DeleteUserAsync(userId);

            if (result.identityResult.Succeeded)
            {
                return Ok(new { message = $"User with email {result.email} has been successfully deleted." });
            }
            else
            {
                return BadRequest(result.identityResult.Errors);
            }
        }
       
        [HttpGet("live-support-users")]
        public async Task<IActionResult> GetLiveSupportUsers()
        {
            var results = await userService.GetLiveSupportUsersAsync();
            return Ok(results);
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var results = await userService.GetAllRolesAsync();
            return Ok(results);
        }
    }
}
