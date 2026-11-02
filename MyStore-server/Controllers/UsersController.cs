using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        var result = await _userService.LoginAsync(dto);
        if (result == null)
            return Unauthorized();
        return Ok(new 
        { 
            user = result.Value.User, 
            token = result.Value.Token 
        });
    }
    
    [Authorize]
    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositDto dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null) return Unauthorized();
        int userId = int.Parse(userIdString);

        try
        {
            var newBalance = await _userService.DepositAsync(userId, dto.Amount);
        
            return Ok(new { balance = newBalance, message = "Успешно пополнено" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [Authorize]
    [HttpPost("purchase")]
    public async Task<IActionResult> Purchase([FromBody] PurchaseDto dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null) return Unauthorized();
        int userId = int.Parse(userIdString);

        try
        {
            var newBalance = await _userService.PurchaseAsync(userId, dto.ProductId, dto.Quantity);
        
            return Ok(new { balance = newBalance, message = "Покупка успешно совершена" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null) return Unauthorized(); 

        int userId = int.Parse(userIdString); 
        var user = await _userService.GetByIdAsync(userId);
        
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null) return Unauthorized(); 

        int userId = int.Parse(userIdString);
        
        try
        {
            var updatedUser = await _userService.UpdateProfileAsync(userId, dto.ImageUrl);
        
            return Ok(updatedUser); 
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
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