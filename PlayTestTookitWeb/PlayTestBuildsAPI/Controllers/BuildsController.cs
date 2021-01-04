using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlayTestBuildsAPI.Controllers
{
    [Route("api/builds")]
    [ApiController]
    public class BuildsController : ControllerBase
    {
        private readonly BuildsService _buildsService;

        public BuildsController(BuildsService buildService) =>
            _buildsService = buildService;

        [HttpGet]
        public ActionResult<List<BuildFile>> Get() =>
            _buildsService.Get();

        [HttpGet("{id}")]
        public ActionResult<BuildFile> Get(string id)
        {
            BuildFile buildFile = _buildsService.Get(id);

            if (buildFile == null)
                return NotFound();

            return buildFile;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public ActionResult<BuildFile> Post()
        {
            IFormFile file = Request.Form.Files.FirstOrDefault();

            if (file == null)
                return Conflict();

            string uniqueFileName = GetUniqueFileName(file.FileName);
            string uploads = Path.Combine(Environment.CurrentDirectory, "uploads");
            string filePath = Path.Combine(uploads, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));

            BuildFile buildFile = new BuildFile
            {
                FileName = file.FileName,
                Path = filePath,
                CreatedOn = new DateTime(),
            };

            return Ok(_buildsService.Create(buildFile));
        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] BuildFile buildFileIn)
        {
            BuildFile buildFile = _buildsService.Get(id);

            if (buildFile == null)
                return NotFound();

            _buildsService.Update(id, buildFileIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            BuildFile buildFile = _buildsService.Get(id);

            if (buildFile == null)
                return NotFound();

            _buildsService.Remove(buildFile.Id);

            return NoContent();
        }
    }
}
