using Argon2id.PasswordHasher;
using AutoMapper;
using MyStore.DTOs;
using MyStore.Exceptions;
using MyStore.Models;
using MyStore.Repositories;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;          
using Microsoft.IdentityModel.Tokens;  
using System.Text;                     

namespace MyStore.Services;

public class UserService : IUserService
{
    private readonly Argon2idPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration; 

    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration) 
    {
        _passwordHasher = new Argon2idPasswordHasher();
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration; 
    }

    public async Task<UserResponseDto?> RegisterAsync(UserRegisterDto dto)
    {
        if (await _userRepository.ExistsByUsernameAsync(dto.Username))
        {
            throw new UsernameAlreadyTakenException(dto.Username);
        }
        if (await _userRepository.ExistsByEmailAsync(dto.Email))
        {
            throw new EmailAlreadyTakenException(dto.Email);
        }
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
        var created = await _userRepository.AddAsync(user);
        return _mapper.Map<UserResponseDto>(created);
    }

    public async Task<(UserResponseDto User, string Token)?> LoginAsync(UserLoginDto dto)
    {
        var user = await _userRepository.GetByLoginAsync(dto.Login);
        if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
        
        var userDto = _mapper.Map<UserResponseDto>(user);
        var token = GenerateJwtToken(user); 

        return (userDto, token); 
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;
        return _mapper.Map<UserResponseDto>(user);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim("name", user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("role", user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2), // Токен живет 2 часа
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}