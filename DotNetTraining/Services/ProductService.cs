using System.Data;
using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Application.Exceptions;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class ProductService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly ProductRepository _repo = new(connection);

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var products = await _repo.GetAllProduct();
            return products;
        }

        public async Task<ProductDto?> GetProductById(Guid productId)
        {
            //entity
            var existingProduct = await _repo.GetProductById(productId);

            if(existingProduct == null)
            {
                throw new Exception("Product not exist");
            }
            // map entity to Dto
            var dto = _mapper.Map<ProductDto>(existingProduct);
            
            return dto;
        }

        public async Task<Product?> UpdateProduct(Guid productId, Product product)
        {
            var existingProduct = await _repo.GetProductById(productId);
            if (existingProduct == null)
            {
                return null; 
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description; 

            return await _repo.UpdateProduct(existingProduct);
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var existingUser = await _repo.GetProductById(productId);
            if (existingUser == null)
            {
                return false; // product không tồn tại
            }

            return await _repo.DeleteProduct(productId);
        }

        public async Task<Product?> CreateProduct(CreateProductDto newProduct)
        {
            // Kiểm tra email đã tồn tại chưa
            var existingUser = await _repo.GetProductByName(newProduct.Name);
            if (existingUser != null)
            {
                return null; // Email đã tồn tại
            }

            // Tạo đối tượng User
            var product = new Product
            {
                Id = Guid.NewGuid(), // Tạo ID mới
                Name = newProduct.Name,
                Price = newProduct.Price,
                Description = newProduct.Description
            };

            // Gọi repository để lưu vào DB
            return await _repo.CreateProduct(product);
        }
    }
}

