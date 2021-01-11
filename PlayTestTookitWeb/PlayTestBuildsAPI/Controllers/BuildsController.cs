using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Services;
using System;

namespace PlayTestBuildsAPI.Controllers
{
    [Route("api/builds")]
    [ApiController]
    public class BuildsController : ControllerBase
    {
        private readonly BuildsService _buildsService;
        private readonly FileService _fileService;

        public BuildsController(BuildsService buildService, FileService fileService)
        {
            _buildsService = buildService;
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult Get() =>
            Ok(_buildsService.Get());

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            BuildFile buildFile = _buildsService.Get(id);

            if (buildFile == null)
                return NotFound();

            byte[] archiveData = _fileService.Get(buildFile.Path);

            return File(archiveData, "application/zip", buildFile.FileName);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public ActionResult<string> Post()
        {
            IFormFile file = Request.Form.Files[0];

            if (file == null)
                return Conflict();

            string filePath = _fileService.Create(file, "/builds/uploads");

            BuildFile buildFile = new BuildFile
            {
                FileName = file.FileName,
                Path = filePath,
                CreatedOn = new DateTime(),
            };

            return Ok(_buildsService.Create(buildFile).Id);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            BuildFile buildFile = _buildsService.Get(id);

            if (buildFile == null)
                return NotFound();

            // TODO remove the build file
            _buildsService.Remove(buildFile.Id);
            _fileService.Delete(buildFile.Path);

            return NoContent();
        }
    }
}
