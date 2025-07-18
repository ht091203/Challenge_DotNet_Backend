using AutoMapper;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Models;
using DotNetTraining.Domains.Entities;

namespace DotNetTraining.AutoMappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>(); 
            //CreateMap<User, UserModel>();
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
            CreateMap<UserRegisterRequest, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<UserLoginRequest, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
