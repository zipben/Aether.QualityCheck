using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmokeAndMirrors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;
        private readonly IAuditEventPublisher _eventPublisher;
        private readonly IErisClient _erisClient;

        public TestController(ILogger<TestController> logger, IAuditEventPublisher eventPublisher, IErisClient erisClient)
        {
            _logger = logger;
            _eventPublisher = eventPublisher;
            _erisClient = erisClient;
        }


        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                await _eventPublisher.CaptureAuditEvent("GetWeather", "1234", "Me", "1", "2");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Failed to Publish to sqs");
                throw;
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Eris")]
        public async Task<IActionResult> Eris()
        {
            return new OkObjectResult(await _erisClient.GetAllPaths());
        }

        [HttpGet("Again")]
        public async Task<IEnumerable<WeatherForecast>> GetAgain()
        {
            await _eventPublisher.CaptureAuditEvent("GetWeather", "1234", "1", "2", Request);

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
