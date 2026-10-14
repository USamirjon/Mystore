using Microsoft.AspNetCore.Mvc;
using MyStore.DTOs;
using MyStore.Services;

namespace MyStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var user = await _userService.RegisterAsync(dto);
        if (user == null)
            return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await _userService.LoginAsync(dto);
        if (user == null)
            return Unauthorized();
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
}