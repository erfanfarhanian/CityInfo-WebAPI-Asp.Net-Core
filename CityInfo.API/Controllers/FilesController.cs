﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        [HttpGet("{fileID}")]
        public ActionResult GetFile(string fileID)
        {
            string pathToFile = "test-file.rar";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);

            if (!fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType)) 
            {
                contentType = "application/octet-stream";
            }

            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
