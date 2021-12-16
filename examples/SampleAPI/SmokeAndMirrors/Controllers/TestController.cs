using Aether.QualityChecks.Models;
using Aether.QualityChecks.TestRunner;
using Microsoft.AspNetCore.Mvc;
using SmokeAndMirrors.QualityChecks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmokeAndMirrors.Controllers
{

    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IQualityCheckRunner _runner;

        public TestController(IQualityCheckRunner runner)
        {
            _runner = runner;
        }

        [HttpGet("all")]
        public async Task<List<QualityCheckResponseModel>> Index()
        {
            
            return await _runner.RunQualityChecks();
        }

        [HttpGet("fail")]
        public async Task<List<QualityCheckResponseModel>> Fail()
        {

            return await _runner.RunQualityChecks<DummyQualityCheckFail>();
        }
    }
}
