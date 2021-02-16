using CsvHelper;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using PlayTestBuildsAPI.Services;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

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


        [HttpGet("{configId}/{projectId}/{export}")]
        public IActionResult Get(string configId, string projectId, string export)
        {
            DataFile project = _dataService.Get(configId).Where((s) => s.Id == projectId).First();

            if (export == "1" && project != null)
            {
                var ms = new MemoryStream();
                var sr = new StreamWriter(ms);
                var csv = new CsvWriter(sr, CultureInfo.InvariantCulture);

                csv.WriteField("StartTimeDate");
                csv.WriteField("Key");
                csv.WriteField("StartTime");
                csv.WriteField("Duration");
                csv.NextRecord();

                bool first = false;

                foreach (var item in project.Input)
                {
                    if (!first)
                    {
                        csv.WriteField(GetDate(project.StartTime));
                        first = true;
                    }
                    else
                        csv.WriteField(" ");

                    csv.WriteField(item.Key);
                    csv.WriteField(item.StartTime);
                    csv.WriteField(item.Duration);
                    csv.NextRecord();
                }

                sr.Flush();

                return File(ms.ToArray(), "text/csv", $"{project.Id}.csv");
            }
            else
                return Conflict();
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

        private string GetDate(double time)
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            DateTime dateTime1 = dateTime.AddMilliseconds(time);
            return dateTime1.ToString();
        }
    }
}
