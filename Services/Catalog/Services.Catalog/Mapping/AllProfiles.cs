using AutoMapper;
using Services.Catalog.Dtos;
using Services.Catalog.Models;

namespace Services.Catalog.Mapping;

public class AllProfiles : Profile
{
    public AllProfiles()
    {
        #region AllProfile

        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();

        #endregion
    }
}