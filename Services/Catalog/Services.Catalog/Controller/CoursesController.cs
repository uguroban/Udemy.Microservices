using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Shared.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Dtos;
using Services.Catalog.Services;

namespace Services.Catalog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [HttpGet($"/api/[controller]/GetAllCourses")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet($"/api/[controller]/GetById/{{id}}")]
        //[Route($"/api/[controller]/GetById={{id}}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResult(response);
        }
        
        [HttpGet($"/api/[controller]/GetByUserId/{{id}}")]
        //[Route($"/api/[controller]/GetByUserId={{id}}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            var response = await _courseService.GetByUserId(id);
            return CreateActionResult(response);
        }
        
        [HttpPost($"/api/[controller]/CreateCourse")]
        public async Task<IActionResult?> CreateCourse(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateCourseAsync(courseCreateDto);
            return CreateActionResult(response);
        }
        
        [HttpPut($"/api/[controller]/UpdateCourse")]
        public async Task<IActionResult?> UpdateCourse(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateCourseAsync(courseUpdateDto);
            return CreateActionResult(response);
        }
        
        [HttpDelete($"{{id}}")]
        public async Task<IActionResult?> DeleteCourse(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResult(response);
        }
        
        
    }
}
