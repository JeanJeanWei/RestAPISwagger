using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleRestAPI.Models;
using SimpleRestAPI.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ColorData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult>  GetAllColorData()
        {

            ColorRepository cp = new ColorRepository();
            var data = await cp.GenerateModel();
            if (data == null)
            {
                return NotFound("No record");
            }
            return Ok(data);
        }
    }
}
