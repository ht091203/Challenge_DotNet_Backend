using AutoMapper;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;

namespace DotNetTraining.AutoMappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>(); // dùng để nhận data
            CreateMap<Product, ProductDto>(); // dùng để trả data
            CreateMap<CreateProductDto, Product>(); // nhận data bên ngoài chuyển thành entity để thêm mới data
            CreateMap<Product, CreateProductDto>(); // trả data ra ngoài che giấu data
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
