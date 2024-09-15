using HST.Business.Services.Security;
using HST.DTO.DTOs.UserDtos;
using HST.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HST.API.Controllers

    {
        [Route("api/[controller]")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenService _tokenService;

            public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenService = tokenService;
            }

            [AllowAnonymous]
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == userLoginDto.PhoneNumber);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                        if (result.Succeeded)
                        {
                            var token = _tokenService.CreateToken(user);
                            return Ok(new { Token = token });
                        }
                        else
                        {
                            return Unauthorized(new { Message = "Telefon numaranız veya şifreniz yanlıştır." });
                        }
                    }
                    else
                    {
                        return Unauthorized(new { Message = "Telefon numaranız veya şifreniz yanlıştır." });
                    }
                }
                return BadRequest(ModelState);
            }

        [Authorize]
            [HttpPost("logout")]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return Ok(new { Message = "Çıkış yapıldı." });
            }

            [Authorize]
            [HttpGet("access-denied")]
            public IActionResult AccessDenied()
            {
                return Forbid();
            }
        }
    }
