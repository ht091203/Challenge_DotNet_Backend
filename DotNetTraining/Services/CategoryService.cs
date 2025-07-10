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

<<<<<<< HEAD
        public async Task<List<CategoryDto>> GetAllCategories() =>
            _mapper.Map<List<CategoryDto>>(await _repo.GetAllCategories());

        public async Task<CategoryDto?> GetCategoriesById(Guid id)
=======
        public async Task<List<CategoryDto>> GetAll() =>
            _mapper.Map<List<CategoryDto>>(await _repo.GetAllCategories());

        public async Task<CategoryDto?> GetById(Guid id)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var data = await _repo.GetCategoryById(id);
            return data == null ? null : _mapper.Map<CategoryDto>(data);
        }

<<<<<<< HEAD
        public async Task<Category?> CreateCategories(CategoryDto dto)
=======
        public async Task<Category?> Create(CategoryDto dto)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var entity = _mapper.Map<Category>(dto);
            return await _repo.CreateCategory(entity);
        }

<<<<<<< HEAD
        public async Task<Category?> UpdateCategories(Guid id, CategoryDto dto)
=======
        public async Task<Category?> Update(Guid id, CategoryDto dto)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var existing = await _repo.GetCategoryById(id) ?? throw new Exception("not found");
            return await _repo.UpdateCategory(_mapper.Map(dto, existing));
        }

<<<<<<< HEAD
        public async Task DeleteCategories(Guid id)
=======
        public async Task Delete(Guid id)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var entity = await _repo.GetCategoryById(id) ?? throw new Exception("not found");
            await _repo.DeleteCategory(entity);
        }
    }
}
