using System.Data;
using Application.Settings;
using AutoMapper;
using Common.Application.CustomAttributes;
using Common.Application.Exceptions;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class CategoryService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly CategoryRepository _repo = new(connection);

        public async Task<List<CategoryDto>> GetAll() =>
            _mapper.Map<List<CategoryDto>>(await _repo.GetAllCategories());

        public async Task<CategoryDto?> GetById(Guid id)
        {
            var data = await _repo.GetCategoryById(id);
            return data == null ? null : _mapper.Map<CategoryDto>(data);
        }

        public async Task<Category?> Create(CategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            return await _repo.CreateCategory(entity);
        }

        public async Task<Category?> Update(Guid id, CategoryDto dto)
        {
            var existing = await _repo.GetCategoryById(id) ?? throw new Exception("not found");
            return await _repo.UpdateCategory(_mapper.Map(dto, existing));
        }

        public async Task Delete(Guid id)
        {
            var entity = await _repo.GetCategoryById(id) ?? throw new Exception("not found");
            await _repo.DeleteCategory(entity);
        }
    }
}
