using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Shared.Controller;
using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : BaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResult<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
