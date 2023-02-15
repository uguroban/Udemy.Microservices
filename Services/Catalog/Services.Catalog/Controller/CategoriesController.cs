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
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet($"/api/[controller]/GetAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            var response =await _categoryService.GetALlAsync();
            return CreateActionResult(response);
        }

        [HttpGet($"/api/[controller]/GetById/{{id}}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByCategoryId(id);
            return CreateActionResult(response);
        }

        [HttpPost($"/api/[controller]/CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateCategory(categoryDto);
            return CreateActionResult(response);
        }


    }
}
