using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleRestAPI.Models;
using SimpleRestAPI.Models.Weather;
using SimpleRestAPI.Repository;
using SimpleRestAPI.Repository.Weather;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IOptions<EnvironmentSettings> options)
        {
            var env = options.Value.Environment;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("SearchByCityName")]
        public IActionResult SearchByCityName(string name)
        {
            WeatherRepositoryDevelopment wp = new WeatherRepositoryDevelopment();
            var data = wp.SearchByCityName(name);
            if (data == null)
            {
                return NotFound("No record");
            }
            return Ok(data);
        }
    }
}
