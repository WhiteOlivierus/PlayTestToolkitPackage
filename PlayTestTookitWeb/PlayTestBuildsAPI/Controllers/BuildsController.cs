using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Services;
using System.Collections.Generic;

namespace PlayTestBuildsAPI.Controllers
{
    [Route("api/builds")]
    [ApiController]
    public class BuildsController : ControllerBase
    {
        private readonly BuildsService _buildsService;

        public BuildsController(BuildsService bookService) =>
            _buildsService = bookService;

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
        public ActionResult<BuildFile> Post([FromBody] BuildFile buildFile)
        {
            _buildsService.Create(buildFile);

            return buildFile;
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] BuildFile buildFileiN)
        {
            BuildFile buildFile = _buildsService.Get(id);

            if (buildFile == null)
                return NotFound();

            _buildsService.Update(id, buildFileiN);

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
