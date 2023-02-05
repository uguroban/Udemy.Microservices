using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Shared.Controller;

public class BaseController : ControllerBase
{
    protected IActionResult CreateActionResult<T>(Response<T>? response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response?.StatusCode
        };
    }
}