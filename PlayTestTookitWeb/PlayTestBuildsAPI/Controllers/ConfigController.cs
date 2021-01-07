using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Services;

namespace PlayTestBuildsAPI.Controllers
{
    [Route("api/config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ConfigService _configService;
        private readonly BuildsService _buildsService;
        private readonly FileService _fileService;

        public ConfigController(ConfigService configService, BuildsService buildService, FileService fileService)
        {
            _buildsService = buildService;
            _fileService = fileService;
            _configService = configService;
        }

        [HttpGet]
        public IActionResult Get() =>
            Ok(_configService.Get());

        [HttpGet("{id}")]
        public IActionResult Get(string id) =>
            Ok(_configService.Get(id));

        [HttpPost]
        public IActionResult Post(ConfigFile configFile) =>
            Ok(_configService.Create(configFile));

        [HttpPut("{id}")]
        public IActionResult Put(string id, ConfigFile configFile)
        {
            configFile.Id = id;
            _configService.Update(id, configFile);
            return Ok(configFile);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            ConfigFile configFile = _configService.Get(id);

            if (configFile != null && !string.IsNullOrEmpty(configFile.BuildId))
                new BuildsController(_buildsService, _fileService).Delete(configFile.BuildId);

            _configService.Remove(id);
            return Ok("Delete succesfully");
        }
    }
}
