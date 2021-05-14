using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewProject.BuisnessLogic;
using NewProject.Model;

namespace NewProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly JeweleryBL _JewelryBL;

        //Use Inyterface for bl

        public WeatherForecastController(ILogger<WeatherForecastController> logger, JeweleryBL JewelryDBContext)
        {
            _logger = logger;
            _JewelryBL = JewelryDBContext;
        }

        [HttpGet("GetForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("GetJewellery/{user}")]
        public ActionResult GetJewellery(string user)
        {
          var response= _JewelryBL.GetUser(user);
            if (response)
                return  this.Ok(response);
            return null;
        }
    }
}
