using ecommercenike_server.Models;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using ecommercenike_server.Services;
using ecommercenike_server.Contracts;

namespace ecommercenike_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService)
    {
      _userService = userService;
      _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var createdUser = await _userService.RegisterUser(registerRequest);
        var token = _jwtService.GenerateToken(createdUser);

        var userResponse = new UserResponse
        {
          Id = createdUser.Id,
          Username = createdUser.Username,
          Email = createdUser.Email,
          Token = token
        };

        return Ok(userResponse);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var user = await _userService.LoginUser(loginRequest);
        var token = _jwtService.GenerateToken(user);

        var userResponse = new UserResponse
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email,
          Token = token
        };

        return Ok(userResponse);
      }
      catch (Exception ex)
      {
        return Unauthorized(new { message = ex.Message });
      }
    }
  }
}