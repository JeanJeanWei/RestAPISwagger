using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        public List<ColorData> GetAllColorData()
        {
            ColorRepository cp = new ColorRepository();
            List<ColorData> list = cp.GenerateModel();
            return list;
        }
    }
}
