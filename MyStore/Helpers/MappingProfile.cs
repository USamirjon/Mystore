using AutoMapper;
using MyStore.DTOs;
using MyStore.Models;

namespace MyStore.Helpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponseDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<User, UserResponseDto>();
        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}