using Argon2id.PasswordHasher;
using AutoMapper;
using MyStore.DTOs;
using MyStore.Exceptions;
using MyStore.Models;
using MyStore.Repositories;
namespace MyStore.Services;

public class UserService : IUserService
{
    private readonly Argon2idPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository  userRepository, IMapper mapper)
    {
        _passwordHasher = new Argon2idPasswordHasher();
        _userRepository = userRepository;
        _mapper = mapper;
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

    public async Task<UserResponseDto?> LoginAsync(UserLoginDto dto)
    {
        var user = await _userRepository.GetByLoginAsync(dto.Login);
        if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;
        return _mapper.Map<UserResponseDto>(user);
    }
}