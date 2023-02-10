using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Shared.Controller;
using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Services.PhotoStock.Dtos;

namespace Services.PhotoStock.Controllers
{
    [Route("api/[controller]/api/[controller]")]
    [ApiController]
    public class PhotosController : BaseController
    {

        [HttpPost($"UploadPhoto")]
        public async Task<IActionResult> PhotoSave(IFormFile? photo, CancellationToken cancellationToken)
        {
            if (photo is { Length: > 0 })
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos", photo.FileName);

                await using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream,cancellationToken);

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResult(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResult(Response<PhotoDto>.Fail("Photo is empty",400));
        }

        [HttpPost($"UploadPhotos")]
        public async Task<IActionResult> PhotosSave(List<IFormFile> files,CancellationToken cancellationToken)
        {
            if (files is not { Count: > 0 })
                return CreateActionResult(Response<PhotoDto>.Fail("Photos is not found", 404));
            foreach (var file in files)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", file.FileName);
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + file.FileName;
                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResult(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResult(Response<PhotoDto>.Fail("Photos is not found", 404));
        }

        [HttpDelete]
        public Task<IActionResult> PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return Task.FromResult(CreateActionResult(Response<NoContent>.Fail("Photo not found", 404)));
            }
            
            System.IO.File.Delete(path);
            return Task.FromResult(CreateActionResult(Response<NoContent>.Success(204)));
        }
    }
    
   
}
