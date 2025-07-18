using AutoMapper;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;

namespace DotNetTraining.AutoMappers
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
