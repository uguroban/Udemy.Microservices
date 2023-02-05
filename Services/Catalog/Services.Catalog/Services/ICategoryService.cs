using Microservices.Shared.Dtos;
using Services.Catalog.Dtos;
using Services.Catalog.Models;

namespace Services.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>?> GetALlAsync();
    Task<Response<CategoryDto>> CreateCategory(CategoryDto categoryDto);
    Task<Response<CategoryDto>> GetByCategoryId(string id);
}