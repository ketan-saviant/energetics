using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.Net.Http.Headers;

namespace WebApplication3.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]

    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment _environment;
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "Ketan" };
        }

        public FileUploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        // POST
        //public async Task<ActionResult> Post(IList<IFormFile> files) - for multiple files
        [HttpPost]
        public async Task<ActionResult> Post(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {

                    var savePath = Path.Combine(_environment.WebRootPath, "uploads", file.FileName);

                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return Created(savePath, file);

                }
                return BadRequest();

                // Multiple File upload part

                //long size = 0;
                //foreach (var file in files)
                //{
                //    var filename = ContentDispositionHeaderValue
                //                    .Parse(file.ContentDisposition)
                //                    .FileName
                //                    .Trim('"');
                //    size += file.Length;
                //    var savePath = Path.Combine(_environment.WebRootPath,"uploads", filename);

                //    using (var fileStream = new FileStream(savePath, FileMode.Create))
                //    {
                //        await file.OpenReadStream().CopyToAsync(fileStream);
                //    }

                //    return Created(savePath, file);
                //}

                //return BadRequest();

                //End of Multiple File upload part
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


    }
}