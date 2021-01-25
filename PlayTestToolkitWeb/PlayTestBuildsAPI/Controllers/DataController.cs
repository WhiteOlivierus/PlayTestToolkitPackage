using Data.Models;
using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Services;

namespace PlayTestBuildsAPI.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly ConfigService _configService;

        public DataController(DataService dataService, ConfigService configService)
        {
            _dataService = dataService;
            _configService = configService;
        }

        [HttpGet]
        public IActionResult Get() =>
            Ok(_dataService.Get());

        [HttpGet("{id}")]
        public IActionResult Get(string id) =>
            Ok(_dataService.Get(id));

        [HttpPost("{id}")]
        public IActionResult Post(string id, DataFile dataFile)
        {
            ConfigFile configFile = _configService.Get(id);

            if (configFile == null)
                return Conflict();

            dataFile.ConfigId = configFile.Id;
            _dataService.Create(dataFile);

            return Ok();
        }
    }
}
