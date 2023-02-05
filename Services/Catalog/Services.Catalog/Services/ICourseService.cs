using Microservices.Shared.Dtos;
using Services.Catalog.Dtos;

namespace Services.Catalog.Services;

public interface ICourseService
{
    Task<Response<List<CourseDto>>?> GetAllAsync();
    Task<Response<CourseDto>> CreateCourseAsync(CourseCreateDto courseCreateDto);
    Task<Response<NoContent>> UpdateCourseAsync(CourseUpdateDto courseUpdateDto);
    Task<Response<CourseDto>> GetByIdAsync(string id);

    Task<Response<List<CourseDto>>> GetByUserId(string id);
    Task<Response<NoContent>> DeleteAsync(string id);
}