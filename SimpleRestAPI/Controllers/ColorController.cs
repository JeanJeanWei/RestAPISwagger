using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SimpleRestAPI.Models;
using SimpleRestAPI.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorController : ControllerBase
    {
        public ColorController(IOptions<EnvironmentSettings> options)
        {
            var env = options.Value.Environment;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ColorData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("AllColorData")]
        public async Task<IActionResult>  AllColorData()
        {

            ColorRepository cp = new ColorRepository();
            var data = await cp.AllColorData();
            if (data == null)
            {
                return NotFound("No record");
            }
            return Ok(data);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ColorData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("ClosestColorNameByHex")]
        public async Task<IActionResult> ClosestColorNameByHex(string hex)
        {

            ColorRepository cp = new ColorRepository();
            var data = await cp.ClosestColorNameByHex(hex);
            if (data == null || data.Name == null)
            {
                return NotFound("No record");
            }
            return Ok(data);
        }
    }
}
