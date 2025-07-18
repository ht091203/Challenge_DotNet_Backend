using AutoMapper;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;

namespace DotNetTraining.AutoMappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
