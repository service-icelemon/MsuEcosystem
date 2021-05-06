using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("UploadArticleImages")]
        public async Task<IActionResult> UploadArticleImages([FromForm] IFormFile[] files)
        {
            List<string> imageLinks = new List<string>();
            string newFileName;
            string path;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    newFileName = String.Format($"{Guid.NewGuid()}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}");
                    path = Path.Combine(_webHostEnvironment.WebRootPath, $"images\\articles\\" + newFileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        imageLinks.Add($"{ Request.Scheme}://{Request.Host}/images/articles/{newFileName}");
                    }
                }
            }
            return Ok(imageLinks);
        }

        [HttpPost("UploadAvatarImage")]
        public async Task<string> UploadAvatarImage([FromForm] IFormFile avatar)
        {
            string newName = String.Format($"{Guid.NewGuid()}{avatar.FileName.Substring(avatar.FileName.LastIndexOf('.'))}");
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images\\users\\avatars\\" + newName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await avatar.CopyToAsync(stream);
            }
            return $"{Request.Scheme}://{Request.Host}/images/users/avatars/{newName}";
        }
    }
}
