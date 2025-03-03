using AutoMapper;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;

namespace DotNetTraining.AutoMappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>(); // dùng để nhận data
            CreateMap<User, UserDto>(); // dùng để trả data
            CreateMap<CreateUserDto, User>(); // nhận data bên ngoài chuyển thành entity để thêm mới data
            CreateMap<User, CreateUserDto>(); // trả data ra ngoài che giấu data
            CreateMap<UpdateUserDto, User>();
        }
    }
}
