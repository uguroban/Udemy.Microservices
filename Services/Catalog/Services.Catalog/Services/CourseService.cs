using System.Net;
using AutoMapper;
using Microservices.Shared.Dtos;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;

namespace Services.Catalog.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        _mapper = mapper;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }

    public async Task<Response<List<CourseDto>>?> GetAllAsync()
    {
        var courses = await _courseCollection.Find(course => true).ToListAsync();
        if (courses.Any())
        {
            foreach (var course in courses)
            {
                await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);


    }

    public async Task<Response<CourseDto>> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        var course = _mapper.Map<Course>(courseCreateDto);
        course.CreatedTime = DateTime.Now;
        await _courseCollection.InsertOneAsync(course);
        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
    }

    public async Task<Response<NoContent>> UpdateCourseAsync(CourseUpdateDto courseUpdateDto)
    {
        var course = _mapper.Map<Course>(courseUpdateDto);

        var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id,course);

        return result == null
            ? Response<NoContent>.Fail("Course does not updated", 404)
            : Response<NoContent>.Success(204);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (course==null)
        {
            return Response<CourseDto>.Fail("Course not found", 404);
        }
        else
        {
          course.Category=  await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
        }
        
        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
    }

    public async Task<Response<List<CourseDto>>> GetByUserId(string id)
    {
        var courses = await _courseCollection.Find<Course>(x => x.UserId == id).ToListAsync();
        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            return Response<List<CourseDto>>.Fail("Course not found", 404);
        }

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var course = await _courseCollection.Find(x => x.Id == id).FirstAsync();
        if (course == null) return Response<NoContent>.Fail("Course not found", 404);

        await _courseCollection.DeleteOneAsync(x => x.Id == id);
        return Response<NoContent>.Success(204);

    }
}