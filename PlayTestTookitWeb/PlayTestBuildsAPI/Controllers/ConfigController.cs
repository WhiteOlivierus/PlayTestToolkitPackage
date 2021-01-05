using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Models;
using PlayTestBuildsAPI.Services;
using System.Collections.Generic;

namespace PlayTestBuildsAPI.Controllers
{
    [Route("api/config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ConfigService _configService;

        public ConfigController(ConfigService configService) =>
            _configService = configService;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post(ConfigFile configFile) =>
            Ok(_configService.Create(configFile));

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
