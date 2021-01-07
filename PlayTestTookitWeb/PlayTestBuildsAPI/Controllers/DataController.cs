using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Services;
using System.Collections.Generic;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

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

        [HttpPut("{id}")]
        public IActionResult Put(string id, DataFile configFile) =>
            Conflict();

        [HttpDelete("{id}")]
        public IActionResult Delete(string id) =>
            Conflict();
    }
}
