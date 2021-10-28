using Aether.Attributes;
using Aether.CustomExtceptions;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Interfaces.ExternalAccessClients;
using Aether.Models.RightRequestWorkflow;
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
        private readonly ICreditV2Client _creditClient;
        private readonly IConsentClient _consentClient;

        public TestController(ILogger<TestController> logger, IAuditEventPublisher eventPublisher, IErisClient erisClient, ICreditV2Client creditClient, IConsentClient consentClient)
        {
            _logger = logger;
            _eventPublisher = eventPublisher;
            _erisClient = erisClient;
            _creditClient = creditClient;
            _consentClient = consentClient;
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

        [HttpGet("Credit/{guid}")]
        public async Task<IActionResult> Credit(string guid)
        {
            var response = await _creditClient.PullCredit(guid);

            return new OkObjectResult(response);
        }

        [HttpGet("Consent/{gcid}")]
        [ParamMetric("gcid")]
        public async Task<IActionResult> Consent(string gcid)
        {
            return new OkObjectResult(1);
        }

        [HttpGet("Eris")]
        public async Task<IActionResult> Eris()
        {
            return new OkObjectResult(await _erisClient.GetAllPaths());
        }

        [HttpPost("Post")]
        [BodyMetric("SubmitEnforcementRequestAsync", typeof(EnforcementRequest), nameof(EnforcementRequest.EnforcementType))]
        public async Task<IActionResult> Post(EnforcementRequest request)
        {
            return new OkObjectResult(request);
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

        [HttpGet("Exception")]
        public async Task<IActionResult> ThrowException()
        {
            throw new HttpStatusCodeException(System.Net.HttpStatusCode.NoContent, "AHHHHHH");
        }
    }
}
