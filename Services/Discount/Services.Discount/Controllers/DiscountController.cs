using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Shared.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Discount.Services;

namespace Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        [Route($"GetAllDiscounts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response =await _discountService.GetAll();
            return CreateActionResult(response);
        }

        [HttpGet]
        [Route($"GetById/{{id}}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _discountService.GetById(id);
            return CreateActionResult(response);
        }

        [HttpPost]
        [Route($"SaveDiscount")]
        public async Task SaveDiscount(Models.Discount discount)
        {
            await _discountService.Save(discount);
        }

        [HttpPut]
        [Route($"UpdateDiscount")]
        public async Task UpdateDiscount(Models.Discount discount)
        {
            await _discountService.Update(discount);
        }

        [HttpDelete]
        [Route($"Delete/{{id}}")]
        public async Task DeleteDiscount(int id)
        {
            await _discountService.Delete(id);
        }

        [HttpGet]
        [Route($"GetByCode/{{code}}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var response = await _discountService.GetByCode(code);
            return CreateActionResult(response);
        }

    
    }
}
