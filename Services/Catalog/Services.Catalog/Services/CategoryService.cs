using System.Net.Security;
using AutoMapper;
using Microservices.Shared.Dtos;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;

namespace Services.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>?> GetALlAsync()
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();
        return categories == null ? null 
            : Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);

    }

    public async Task<Response<CategoryDto>> CreateCategory(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryCollection.InsertOneAsync(category);
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
    }

    public async Task<Response<CategoryDto>> GetByCategoryId(string id)
    {
        var category = await _categoryCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
        return category == null
            ? Response<CategoryDto>.Fail("Category not found", 404)
            : Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }
}