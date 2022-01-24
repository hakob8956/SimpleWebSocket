using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketHost.Hubs;

namespace WebSocketHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHubContext<NotifyHub> _hub;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IHubContext<NotifyHub> hub, ILogger<WeatherForecastController> logger)
        {
            _hub = hub;
            _logger = logger;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet("send")]
        public async Task<IActionResult> Send(string message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok();
        }

        [HttpGet]
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
    }
}
